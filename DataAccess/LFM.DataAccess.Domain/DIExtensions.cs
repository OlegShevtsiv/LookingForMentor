using LFM.DataAccess.Domain.Services.Role;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.DataAccess.Domain
{
    public static class DIExtensions
    {
        public static void AddDomainDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<ILfmRoleManager, LfmRoleManager>();
        }
    }
}