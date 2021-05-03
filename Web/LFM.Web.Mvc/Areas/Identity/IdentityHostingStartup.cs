using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LFM.Web.Areas.Identity.IdentityHostingStartup))]
namespace LFM.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}