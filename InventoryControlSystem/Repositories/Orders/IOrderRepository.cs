﻿using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(string id);

        Task CreateOrder(Order order);

        Task<bool> DeleteOrder(string id);

        Task<IEnumerable<Order>> GetAllOrders();

        Task<bool> UpdateOrder(Order orderChanges);

        Task<IEnumerable<Order>> ToOrder();

        Task<IEnumerable<Order>> ToFulfill();
        Task<IEnumerable<Order>> ToOrderList();

    }
}
