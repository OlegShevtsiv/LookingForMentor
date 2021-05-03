using System.Threading.Tasks;
using Lfm.Core.Common.Web.Configurations;
using Lfm.Domain.Common.Caching.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Lfm.Web.Mvc.App.Middlewares
{
    public class UserCacheMiddleware
    {
        private readonly RequestDelegate _next;
        
        public UserCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Handles request
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IUserCachingService userCachingService)
        {
            Endpoint endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }
            
            if (context.User.Identity.IsAuthenticated)
            {
                await userCachingService.EnsureUserCache();
            }

            await _next(context);
        }
    }
}