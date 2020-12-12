using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface IItemRepository
    {
        Item GetItem(int ID);

        Item CreateItem(Item item);

        Item DeleteItem(int ID);

        IEnumerable<Item> GetAllItems();

        Item UpdateItem(Item bottleChanges);

    }
}
