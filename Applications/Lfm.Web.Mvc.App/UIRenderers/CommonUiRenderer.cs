using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class CommonUiRenderer
    {
        public static string ActiveLink(HttpContext context, string controllerName, string actionName)
        {
            if (context.GetRouteValue("controller").ToString() == controllerName &&
                context.GetRouteValue("action").ToString() == actionName)
            {
                return "active";
            }
            return string.Empty;
        }
    }
}