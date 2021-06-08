using System.Security.Claims;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Lfm.Domain.Common.Identity.Claims
{
    public class LfmUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<LfmUser, LfmRole> 
    {
        public LfmUserClaimsPrincipalFactory(
            UserManager<LfmUser> userManager, 
            RoleManager<LfmRole> roleManager, 
            IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {
        }
        
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(LfmUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));    
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            return identity;
        }
    }
}