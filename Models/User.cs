using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryControlSystem.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Auth0ID { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }

    }
}
