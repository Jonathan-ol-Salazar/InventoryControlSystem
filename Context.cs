using Microsoft.Extensions.Options;
using MongoDB.Driver;
using InventoryControlSystem.Models;
namespace InventoryControlSystem
{
    public class Context : IUserContext
    {
        private readonly IMongoDatabase mongoDatabase;

        public Context(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client.GetDatabase(options.Value.Database);
        }

        //public Context(IConfiguration config)
        //{
        //    MongoClient client = new MongoClient(config.GetConnectionString("ConnectionString"));
        //    mongoDatabase = client.GetDatabase("Database");

        //}


        public IMongoCollection<User> Users => mongoDatabase.GetCollection<User>("Users");
        //public IMongoCollection<Project> Projects => mongoDatabase.GetCollection<Project>("Projects");
        //public IMongoCollection<Issue> Issues => mongoDatabase.GetCollection<Issue>("Issues");
        //public IMongoCollection<Role> Roles => mongoDatabase.GetCollection<Role>("Roles");

    }


    // Add interfaces for Context

    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }
    }

}
