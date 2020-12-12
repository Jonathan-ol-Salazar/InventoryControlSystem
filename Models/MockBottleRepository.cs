using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class MockBottleRepository : IBottleRepository
    {
        private List<Bottle> _bottleList;

        public MockBottleRepository()
        {
            _bottleList = new List<Bottle>
            {
                new Bottle() {ID = 1, Name = "Jack Daniels", Type = "Whiskey", Quantity = 1, Price = 1},
                new Bottle() {ID = 2, Name = "Smirnoff", Type = "Vodka", Quantity = 1, Price = 1}
            };
        }

        public Bottle CreateBottle(Bottle bottle)
        {
            throw new NotImplementedException();
        }

        public Bottle DeleteBottle(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bottle> GetAllBottles()
        {
            throw new NotImplementedException();
        }

        public Bottle GetBottle(int ID)
        {
            return _bottleList.FirstOrDefault(e => e.ID == ID);
            
        }

        public Bottle UpdateBottle(Bottle bottleChanges)
        {
            throw new NotImplementedException();
        }
    }
}
