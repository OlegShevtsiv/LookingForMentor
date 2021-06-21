using Lfm.Domain.Common.Caching.User;
using Lfm.Domain.Common.Services.Role;
using Microsoft.Extensions.DependencyInjection;

namespace Lfm.Domain.Common
{
    public static class ModuleExporter
    {
        public static void AddDomainCommonServices(this IServiceCollection services)
        {
            services.AddTransient<ILfmRoleManager, LfmRoleManager>();
            services.AddSingleton<IUserCachingService, UserCachingService>();
        }
    }
}