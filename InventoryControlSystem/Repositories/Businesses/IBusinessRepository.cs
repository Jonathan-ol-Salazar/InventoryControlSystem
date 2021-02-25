using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Businesses
{
    public interface IBusinessRepository
    {
        Task<Business> GetBusiness(string id);

        Task CreateBusiness(Business business);

        Task<bool> DeleteBusiness(string id);

        Task<IEnumerable<Business>> GetAllBusinesss();

        Task<bool> UpdateBusiness(Business businessChanges);
        Task<Business> GetSelectedBusiness();

    }
}
