using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace InventoryControlSystem.Models
{
    public class OrderList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public string SupplierName { get; set; }

        public string Business { get; set; }

        public List<string> ProductsID { get; set; }

        public List<string> OrdersID { get; set; }


        public int Price { get; set; }
        public DateTime OrderDate  { get; set; }

        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public bool Confirmed { get; set; }
  

}
}
