using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using InventoryControlSystem.Models;

using InventoryControlSystem.Repositories.Users;
using InventoryControlSystem.Repositories.Products;
using InventoryControlSystem.Repositories.Customers;
using InventoryControlSystem.Repositories.Suppliers;
using InventoryControlSystem.Repositories.Orders;
using InventoryControlSystem.Repositories.OrderLists;
using InventoryControlSystem.Repositories.Funds;
using InventoryControlSystem.Repositories.Roles;
using InventoryControlSystem.Repositories.Businesses;

using InventoryControlSystem.Repositories.InvoiceBusinesses;
using InventoryControlSystem.Repositories.InvoiceCustomers;

namespace InventoryControlSystem
{
    public class Startup
	{
		//private IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDbContextPool<AppDbContext>(
			//	options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

			services.Configure<Settings>(options =>
			{
				options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
				options.Database = Configuration.GetSection("MongoDB:Database").Value;
			});

			services.AddMvc();
			services.AddControllersWithViews();
			services.AddRazorPages().AddRazorRuntimeCompilation();

			services.AddTransient<IUserContext, Context>();
			services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IProductContext, Context>();
            services.AddTransient<IProductRepository, ProductRepository>();

			services.AddTransient<ICustomerContext, Context>();
			services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<ISupplierContext, Context>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();

            services.AddTransient<IOrderContext, Context>();
			services.AddTransient<IOrderRepository, OrderRepository>();

			services.AddTransient<IOrderListContext, Context>();
			services.AddTransient<IOrderListRepository, OrderListRepository>();

			services.AddTransient<IFundContext, Context>();
			services.AddTransient<IFundRepository, FundRepository>();

			services.AddTransient<IRoleContext, Context>();
			services.AddTransient<IRoleRepository, RoleRepository>();

			services.AddTransient<IBusinessContext, Context>();
			services.AddTransient<IBusinessRepository, BusinessRepository>();

            services.AddTransient<IInvoiceCustomerContext, Context>();
            services.AddTransient<IInvoiceCustomerRepository, InvoiceCustomerRepository>();

            services.AddTransient<IInvoiceBusinessContext, Context>();
            services.AddTransient<IInvoiceBusinessRepository, InvoiceBusinessRepository>();

            // Auth0 

            // Cookie configuration for HTTP to support cookies with SameSite=None
            //services.ConfigureSameSiteNoneCookies();

            // Cookie configuration for HTTPS
            services.Configure<CookiePolicyOptions>(options =>
            {
				options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            // Add authentication services
            services.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
			.AddCookie()
			.AddOpenIdConnect("Auth0", options => {
				// Set the authority to your Auth0 domain
				options.Authority = $"https://{Configuration["Auth0:Domain"]}";

				// Configure the Auth0 Client ID and Client Secret
				options.ClientId = Configuration["Auth0:ClientId"];
				options.ClientSecret = Configuration["Auth0:ClientSecret"];

				// Set response type to code
				options.ResponseType = OpenIdConnectResponseType.Code;

				// Configure the scope
				options.Scope.Clear();
				options.Scope.Add("openid");
				options.Scope.Add("profile");
				options.Scope.Add("email");
				options.Scope.Add("name");
				options.Scope.Add("picture");



				// Set the correct name claim type
				options.TokenValidationParameters = new TokenValidationParameters
				{
					NameClaimType = "name",
					RoleClaimType = "https://schemas.bottleo-ics.com/roles"
				};

				// Set the callback path, so Auth0 will call back to http://localhost:3000/callback
				// Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
				options.CallbackPath = new PathString("/Home/Index/callback");

				// Configure the Claims Issuer to be Auth0
				options.ClaimsIssuer = "Auth0";


				// Saves tokens to the AuthenticationProperties
				options.SaveTokens = true;

				// Handling logout redirect
				options.Events = new OpenIdConnectEvents
				{
					// handle the logout redirection
					OnRedirectToIdentityProviderForSignOut = (context) =>
					{
						var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

						var postLogoutUri = context.Properties.RedirectUri;
						if (!string.IsNullOrEmpty(postLogoutUri))
						{
							if (postLogoutUri.StartsWith("/"))
							{
								// transform to absolute
								var request = context.Request;
								postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
							}
							logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
						}

						context.Response.Redirect(logoutUri);
						context.HandleResponse();

						return Task.CompletedTask;
					},

					OnRedirectToIdentityProvider = context =>
					{
						// The context's ProtocolMessage can be used to pass along additional query parameters
						// to Auth0's /authorize endpoint.
						// 
						// Set the audience query parameter to the API identifier to ensure the returned Access Tokens can be used
						// to call protected endpoints on the corresponding API.
						context.ProtocolMessage.SetParameter("audience", Configuration["Auth0:Audience"]);

						return Task.FromResult(0);
					}
				};


			});


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else 
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Account}/{action=Login}");
			});
		}
	}
}
