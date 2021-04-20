using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;
using Microsoft.AspNetCore.Identity;

namespace LFM.DataAccess.Domain.Services.Role
{
    internal class LfmRoleManager : ILfmRoleManager
    {
        private readonly RoleManager<LfmRole> _roleManager;
        private readonly UserManager<LfmUser> _userManager;

        public LfmRoleManager(
            RoleManager<LfmRole> roleManager, 
            UserManager<LfmUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<LfmRole> GetRole(IdentityRoles roleType)
        {
            return await _roleManager.FindByNameAsync(roleType.ToString());
        }

        public async Task<bool> IsUserInRole(LfmUser user, IdentityRoles roleType)
        {
            return await _userManager.IsInRoleAsync(user, roleType.ToString());
        }

        public async Task SetRoleToUser(LfmUser user, IdentityRoles roleType)
        {
            await _userManager.AddToRoleAsync(user, roleType.ToString());
        }
        
        public async Task RiseRoleFromUser(LfmUser user, IdentityRoles roleType)
        {
            await _userManager.RemoveFromRoleAsync(user, roleType.ToString());
        }
    }
}