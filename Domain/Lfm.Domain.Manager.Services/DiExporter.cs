using Lfm.Domain.Manager.Services.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Lfm.Domain.Manager.Services
{
    public static class DiExporter
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ManagersModelsMaps));
        }
    }
}