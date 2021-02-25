using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace InventoryControlSystem.Models
{
    public class Fund
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ID { get; set; }
        public double TotalRevenue { get; set; } = 0;
        public double TodaysRevenue { get; set; } = 0;
        public double TotalProfit { get; set; } = 0;
        public double TodaysProfit { get; set; } = 0;
        public double TotalSales { get; set; } = 0;
        public double TodaysSales { get; set; } = 0;
        public double TotalCost { get; set; } = 0;
        public double TodaysCost { get; set; } = 0;

        public DateTime DateLastCalculated { get; set; }



    }
}
