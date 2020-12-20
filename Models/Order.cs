using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public int NumProducts { get; set; }
        public List<string> ProductsID { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public bool Fulfilled { get; set; }
        public bool Ordered { get; set; }
        public bool OrderList { get; set; }
        public DateTime OrderDate { get; set; }


    }
}
