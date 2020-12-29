using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace InventoryControlSystem.Models
{
    public class InvoiceCustomer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public List<Order> CustomerOrders { get; set; } = new List<Order>();
    }
}
