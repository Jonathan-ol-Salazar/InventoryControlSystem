using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace InventoryControlSystem.Models
{
    public class InvoiceBusiness
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public List<OrderList> BusinessOrders { get; set; } = new List<OrderList>();
    }
}
