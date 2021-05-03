using LFM.DataAccess.DB.Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.DataAccess.DB.Core
{
    public static class DiExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}