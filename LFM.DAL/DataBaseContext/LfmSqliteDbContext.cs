using LFM.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LFM.DAL.DataBaseContext
{
    public class LfmSqliteDbContext : IdentityDbContext<LfmUser, IdentityRole<int>, int>
    {
        public DbSet<LfmUser> LfmUsers { get; set; }

        public LfmSqliteDbContext()
        {
            Database.EnsureCreated();
        }

        public LfmSqliteDbContext(DbContextOptions<LfmSqliteDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
