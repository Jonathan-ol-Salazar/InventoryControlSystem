using Microsoft.Extensions.Options;
using MongoDB.Driver;
using InventoryControlSystem.Models;
namespace InventoryControlSystem
{
    public class Context : IUserContext, IProductContext
    {
        private readonly IMongoDatabase mongoDatabase;

        public Context(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<User> Users => mongoDatabase.GetCollection<User>("Users");
        public IMongoCollection<Product> Products => mongoDatabase.GetCollection<Product>("Products");
        //public IMongoCollection<Issue> Issues => mongoDatabase.GetCollection<Issue>("Issues");
        //public IMongoCollection<Role> Roles => mongoDatabase.GetCollection<Role>("Roles");

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

}
