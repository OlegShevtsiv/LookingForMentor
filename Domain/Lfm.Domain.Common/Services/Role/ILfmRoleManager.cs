using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using LFM.DataAccess.DB.Core.Types;

namespace Lfm.Domain.Common.Services.Role
{
    public interface ILfmRoleManager
    {
        Task<LfmRole> GetRole(LfmIdentityRolesEnum roleType);

        Task<bool> IsUserInRole(LfmUser user, LfmIdentityRolesEnum roleType);

        Task SetRoleToUser(LfmUser user, LfmIdentityRolesEnum roleType);

        Task RiseRoleFromUser(LfmUser user, LfmIdentityRolesEnum roleType);

        Task<LfmIdentityRolesEnum> RetrieveUserRole(int userId);
    }
}