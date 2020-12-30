using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
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
        public string OrderID { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderEmail { get; set; }
        public int SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverEmail { get; set; }
        public int ReceiverPhone { get; set; }
        public DateTime Date { get; set; }
        public List<string> ProductsID { get; set; }
        public double TotalCost { get; set; }
    }
}
