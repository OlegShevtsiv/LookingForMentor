using Lfm.Core.Common.Web.Configurations;
using Microsoft.Extensions.Configuration;

namespace Lfm.Core.Common.Web.Extensions
{
    public static class ConfigurationsExtensions
    {
        public static AppConfigurations GetAppConfigurations(this IConfiguration configuration)
        {
            var appConfiguration = configuration.GetSection("AppConfigurations").Get<AppConfigurations>();
            return appConfiguration;
        }
    }
}