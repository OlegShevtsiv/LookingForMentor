using LFM.DataAccess.DB.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.Core.Context
{
    public abstract class LfmDbContext<TContext> : IdentityDbContext<LfmUser, LfmRole, int>
        where TContext : DbContext
    {
        public DbSet<LfmUser> LfmUsers { get; set; }
        public DbSet<LfmRole> LfmRoles { get; set; }

        public LfmDbContext()
        {
            Database.EnsureCreated();
        }
        
        public LfmDbContext(DbContextOptions<TContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelsConfiguration.ConfigureRelations(builder);
        }
    }
}
