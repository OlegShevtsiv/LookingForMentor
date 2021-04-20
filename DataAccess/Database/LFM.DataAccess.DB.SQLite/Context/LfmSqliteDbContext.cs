using LFM.DataAccess.DB.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace LFM.DataAccess.DB.SQLite.Context
{
    public class LfmSqliteDbContext : LfmDbContext<LfmSqliteDbContext>
    {
        public LfmSqliteDbContext(DbContextOptions<LfmSqliteDbContext> options) : base(options)
        {
        }
    }
}