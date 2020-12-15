using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface IOrderListRepository
    {
        OrderList GetOrderList(int ID);

        OrderList CreateOrderList(OrderList orderList);

        OrderList DeleteOrderList(int ID);

        IEnumerable<OrderList> GetAllOrderLists();

        OrderList UpdateOrderList(OrderList orderListChanges);
    }
}
