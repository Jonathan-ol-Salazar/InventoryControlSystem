using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    interface IOrderRepository
    {
        Order GetOrder(int ID);

        Order CreateOrder(Order order);

        Order DeleteOrder(int ID);

        IEnumerable<Order> GetAllOrders();

        Order UpdateOrder(Order orderChanges);
    }
}
