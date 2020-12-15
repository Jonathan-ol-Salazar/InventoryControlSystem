using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class OrderListRepository : IOrderListRepository
    { 
        private readonly AppDbContext context;

        public OrderListRepository(AppDbContext context)
        {
            this.context = context;
        }

        public OrderList CreateOrderList(OrderList orderList)
        {
            context.OrderLists.Add(orderList);
            context.SaveChanges();
            return orderList;
        }

        public OrderList DeleteOrderList(int ID)
        {
            OrderList orderList = context.OrderLists.Find(ID);
            if (orderList != null)
            {
                context.OrderLists.Remove(orderList);
                context.SaveChanges();
            }
            return orderList;
        }

        public IEnumerable<OrderList> GetAllOrderLists()
        {
            return context.OrderLists;
        }

        public OrderList GetOrderList(int ID)
        {
            return context.OrderLists.Find(ID);
        }

        public OrderList UpdateOrderList(OrderList orderListChanges)
        {
            var orderList = context.OrderLists.Attach(orderListChanges);

            orderList.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return orderListChanges;
        }
    }
}
