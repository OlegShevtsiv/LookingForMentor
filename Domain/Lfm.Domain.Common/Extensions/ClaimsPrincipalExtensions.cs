using System;
using System.Security.Claims;
using LFM.Core.Common.Data;
using LFM.Core.Common.Exceptions;
using LFM.DataAccess.DB.Core.Types;

namespace Lfm.Domain.Common.Extensions
{
    public static class LfmUserClaimsPrincipalExtensions
    {
        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            return GetClaimValue(claimsPrincipal, ClaimTypes.GivenName);
        }
        
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return GetClaimValue(claimsPrincipal, ClaimTypes.Email);
        }
        
        public static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
        {
            return GetClaimValue(claimsPrincipal, ClaimTypes.MobilePhone);
        }
        
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            string claimValue = GetClaimValue(claimsPrincipal, ClaimTypes.NameIdentifier);
            if (!int.TryParse(claimValue, out int id))
            {
                throw new LfmException(Messages.InvalidUserClaim);
            }
            return id;
        }
        
        public static LfmIdentityRolesEnum GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            string claimValue = GetClaimValue(claimsPrincipal, ClaimTypes.Role);
            if (!Enum.TryParse(claimValue, ignoreCase: true, out LfmIdentityRolesEnum role))
            {
                throw new LfmException(Messages.InvalidUserClaim);
            }
            return role;
        }

        private static string GetClaimValue(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            if (claimsPrincipal.HasClaim(c => c.Type == claimType))
            {
                return claimsPrincipal.FindFirst(claimType).Value;
            }
            string errorMessage = string.Format(Messages.UserClaimNotFound, claimType);
            throw new LfmException(errorMessage);
        }
    }
}