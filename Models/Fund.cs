using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryControlSystem.Models
{
    public class Fund
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public double Funds { get; set; } = 0;
    }
}
