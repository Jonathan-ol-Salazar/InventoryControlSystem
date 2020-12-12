using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public interface IBottleRepository
    {
        Bottle GetBottle(int ID);

        Bottle CreateBottle(Bottle bottle);

        Bottle DeleteBottle(int ID);

        IEnumerable<Bottle> GetAllBottles();

        Bottle UpdateBottle(Bottle bottleChanges);

    }
}
