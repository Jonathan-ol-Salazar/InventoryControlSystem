using InventoryControlSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Funds
{
    public interface IFundRepository
    {
        Task<Fund> GetFund(string id);

        Task CreateFund(Fund fund);

        Task<bool> DeleteFund(string id);

        Task<IEnumerable<Fund>> GetAllFunds();

        Task<bool> UpdateFund(Fund fundChanges);

    }
}
