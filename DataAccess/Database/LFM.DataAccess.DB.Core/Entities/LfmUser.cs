using Microsoft.AspNetCore.Identity;

namespace LFM.DataAccess.DB.Core.Entities
{
    public class LfmUser : IdentityUser<int>
    {
        public int Age { get; set; }
    }
}