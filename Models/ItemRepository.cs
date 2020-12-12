using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        public ItemRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Item CreateItem(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }

        public Item DeleteItem(int ID)
        {
            Item item = context.Items.Find(ID);
            if (item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return context.Items;
        }

        public Item GetItem(int ID)
        {
            return context.Items.Find(ID);
        }

        public Item UpdateItem(Item itemChanges)
        {
            var item = context.Items.Attach(itemChanges);

            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return itemChanges;
        }
    }
}
