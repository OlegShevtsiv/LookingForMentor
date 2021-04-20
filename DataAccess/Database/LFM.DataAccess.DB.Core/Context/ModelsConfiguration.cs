using LFM.DataAccess.DB.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.Core.Context
{
    public static class ModelsConfiguration
    {
        public static void ConfigureRelations(ModelBuilder builder)
        {
            #region Identity

            builder.Entity<LfmUser>(m =>
            {
                m.ToTable("LfmUsers");
            });

            builder.Entity<LfmRole>(m =>
            {
                m.ToTable("LfmRoles");
            });
            
            builder.Entity<IdentityRoleClaim<int>>(m =>
            {
                m.ToTable("LfmRoleClaims");
            });
            
            builder.Entity<IdentityUserRole<int>>(m =>
            {
                m.ToTable("LfmUserRoles");
            });
            
            builder.Entity<IdentityUserClaim<int>>(m =>
            {
                m.ToTable("LfmUserClaims");
            });
            
            builder.Entity<IdentityUserToken<int>>(m =>
            {
                m.ToTable("LfmUserTokens");
            });
            
            builder.Entity<IdentityUserLogin<int>>(m =>
            {
                m.ToTable("LfmUserLogins");
            });

            #endregion
        }
    }
}