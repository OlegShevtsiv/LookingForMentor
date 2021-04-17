using Microsoft.AspNetCore.Identity;

namespace LFM.DAL.Entities
{
    public class LfmUser : IdentityUser<int>
    {
        public int Age { get; set; }
    }
}