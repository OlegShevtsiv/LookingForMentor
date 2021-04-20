using Microsoft.AspNetCore.Identity;

namespace LFM.DataAccess.DB.Core.Entities
{
    public class LfmRole : IdentityRole<int>
    {
        public LfmRole() : base()
        {
        }
        
        public LfmRole(string roleName)
            : base(roleName)
        {
        }
    }
}