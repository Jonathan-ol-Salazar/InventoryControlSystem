using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }


        public Order CreateOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order DeleteOrder(int ID)
        {
            Order order = context.Orders.Find(ID);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders;
        }

        public Order GetOrder(int ID)
        {
            return context.Orders.Find(ID);
        }

        public Order UpdateOrder(Order orderChanges)
        {
            var order = context.Orders.Attach(orderChanges);

            order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return orderChanges;
        }
    }
}
