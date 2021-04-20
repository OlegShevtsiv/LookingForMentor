using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;

namespace LFM.DataAccess.Domain.Services.Role
{
    public interface ILfmRoleManager
    {
        Task<LfmRole> GetRole(IdentityRoles roleType);

        Task<bool> IsUserInRole(LfmUser user, IdentityRoles roleType);

        Task SetRoleToUser(LfmUser user, IdentityRoles roleType);

        Task RiseRoleFromUser(LfmUser user, IdentityRoles roleType);
    }
}