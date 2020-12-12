using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class BottleRepository : IBottleRepository
    {
        private readonly AppDbContext context;

        public BottleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Bottle CreateBottle(Bottle bottle)
        {
            context.Bottles.Add(bottle);
            context.SaveChanges();
            return bottle;
        }

        public Bottle DeleteBottle(int ID)
        {
            Bottle bottle = context.Bottles.Find(ID);
            if (bottle != null)
            {
                context.Bottles.Remove(bottle);
                context.SaveChanges();
            }
            return bottle;
        }

        public IEnumerable<Bottle> GetAllBottles()
        {
            return context.Bottles;
        }

        public Bottle GetBottle(int ID)
        {
            return context.Bottles.Find(ID);
        }

        public Bottle UpdateBottle(Bottle bottleChanges)
        {
            var bottle = context.Bottles.Attach(bottleChanges);

            bottle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();


            return bottleChanges;
        }
    }
}
