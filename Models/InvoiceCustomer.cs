﻿using MongoDB.Bson;
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
        public Order Order { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string RecieverName { get; set; }
        public string RecieverAddress { get; set; }
        public string RecieverEmail { get; set; }
        public string RecieverPhone { get; set; }
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }
        public double TotalCost { get; set; }
    }
}