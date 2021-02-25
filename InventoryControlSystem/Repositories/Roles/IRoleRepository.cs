using InventoryControlSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task<Role> GetRole(string id);

        Task CreateRole(Role role);

        Task<bool> DeleteRole(string id);

        Task<IEnumerable<Role>> GetAllRoles();

        Task<bool> UpdateRole(Role roleChanges);
    }
}
