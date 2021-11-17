using Lfm.Domain.Manager.Services.DataProviders;
using Lfm.Domain.Manager.Services.DataProviders.Implementations;
using Lfm.Domain.Manager.Services.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Lfm.Domain.Manager.Services
{
    public static class DiExporter
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ManagersModelsMaps));
            services.AddScoped<IToDoProvider, ToDoProvider>();
        }
    }
}