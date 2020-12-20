using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.OrderLists
{
    public interface IOrderListRepository
    {
        Task<OrderList> GetOrderList(string id);

        Task CreateOrderList(OrderList orderList);

        Task<bool> DeleteOrderList(string id);

        Task<IEnumerable<OrderList>> GetAllOrderLists();

        Task<bool> UpdateOrderList(OrderList orderListChanges);

        Task<OrderList> OrderListExist(string supplier);

    }
}
