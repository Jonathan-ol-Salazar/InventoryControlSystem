﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
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
        public string OrderListID { get; set; }
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
