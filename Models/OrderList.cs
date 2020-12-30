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
        public string SuppliersName { get; set; }
        public string SuppliersID { get; set; }
        public string SuppliersAddress { get; set; }
        public int SuppliersPhone { get; set; }
        public string SuppliersEmail { get; set; }
        public string BusinessesName { get; set; }
        public string BusinessesID { get; set; }
        public int BusinessesPhone { get; set; }
        public string BusinessesEmail { get; set; }
        public string BusinessesAddress { get; set; }
        public List<string> ProductsID { get; set; }
        public List<string> OrdersID { get; set; }
        public double TotalCost { get; set; }
        public DateTime OrderDate  { get; set; }
        public bool Confirmed { get; set; }
  

}
}
