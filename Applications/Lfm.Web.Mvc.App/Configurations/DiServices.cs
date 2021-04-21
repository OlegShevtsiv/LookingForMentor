using System;
using LFM.DataAccess.DB.SQLite;
using LFM.DataAccess.Domain;
using LFM.Domain.Write;
using Lfm.Web.Mvc.App.Mapper;
using Lfm.Web.Mvc.App.Mapper.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lfm.Web.Mvc.App.Configurations
{
    public static class DiServices
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("SqliteDbConnectionPath");
            services.AddLfmSqliteContext(connection);
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            
            services.AddCommands();
            services.AddAutoMapper(typeof(ViewModelsCommandsMapperConfigurations));
            
            services.AddDomainDataAccessServices();
            
            services.AddControllersWithViews();
            services.AddRazorPages();
        }
    }
}