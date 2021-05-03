using System;
using Microsoft.AspNetCore.Http;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class CommonUiRenderer
    {
        public static string ActiveLink(PathString requestPath, string link)
        {
            link = link.Trim('/');
            string currentPath = requestPath.ToString().Trim('/');
            if (link.Equals(currentPath, StringComparison.InvariantCultureIgnoreCase))
            {
                return "active";
            }
            return string.Empty;
        }
    }
}