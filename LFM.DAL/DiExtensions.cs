using LFM.DAL.DataBaseContext;
using LFM.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.DAL
{
    public static class DiExtensions
    {
        public static void AddLfmSqliteContext(this IServiceCollection services, string sqliteDbPath)
        {
            services.AddDbContext<LfmSqliteDbContext>(options =>
            {
                SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = sqliteDbPath;
                
                options.UseSqlite(connectionStringBuilder.ToString());
            });
            
            services.AddIdentity<LfmUser, IdentityRole<int>>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<LfmSqliteDbContext>();
        }
    }
}