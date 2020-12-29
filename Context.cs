using Microsoft.Extensions.Options;
using MongoDB.Driver;
using InventoryControlSystem.Models;

namespace InventoryControlSystem
{
    public class Context : IUserContext, IProductContext, ICustomerContext, ISupplierContext, IOrderContext, IOrderListContext, IFundContext, IRoleContext, IBusinessContext, IInvoiceContext
    {
        private readonly IMongoDatabase mongoDatabase;

        public Context(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<User> Users => mongoDatabase.GetCollection<User>("Users");
        public IMongoCollection<Product> Products => mongoDatabase.GetCollection<Product>("Products");
        public IMongoCollection<Customer> Customers => mongoDatabase.GetCollection<Customer>("Customers");
        public IMongoCollection<Supplier> Suppliers => mongoDatabase.GetCollection<Supplier>("Suppliers");
        public IMongoCollection<Order> Orders => mongoDatabase.GetCollection<Order>("Orders");
        public IMongoCollection<OrderList> OrderLists => mongoDatabase.GetCollection<OrderList>("OrderLists");
        public IMongoCollection<Fund> Funds => mongoDatabase.GetCollection<Fund>("Funds");
        public IMongoCollection<Role> Roles => mongoDatabase.GetCollection<Role>("Roles");
        public IMongoCollection<Business> Businesses => mongoDatabase.GetCollection<Business>("Businesses");
        public IMongoCollection<Invoice> Invoices => mongoDatabase.GetCollection<Invoice>("Invoices");



    }


    // Add interfaces for Context

    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }
    }

    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }

    public interface ICustomerContext
    {
        IMongoCollection<Customer> Customers { get; }
    }

    public interface ISupplierContext
    {
        IMongoCollection<Supplier> Suppliers { get; }
    }
    public interface IOrderContext
    {
        IMongoCollection<Order> Orders { get; }
    }
    public interface IOrderListContext
    {
        IMongoCollection<OrderList> OrderLists { get; }
    }
    public interface IFundContext
    {
        IMongoCollection<Fund> Funds { get; }
    }
    public interface IRoleContext
    {
        IMongoCollection<Role> Roles{ get; }
    }
    public interface IBusinessContext
    {
        IMongoCollection<Business> Businesses { get; }
    }
    public interface IInvoiceContext
    {
        IMongoCollection<Invoice> Invoices { get; }
    }
}
