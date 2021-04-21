using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LFM.Core.Common.Exceptions;
using Lfm.Core.Common.Web.Data;
using Lfm.Web.Mvc.App.SessionAlerts;
using Microsoft.AspNetCore.Http;

namespace Lfm.Web.Mvc.App.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (LfmException lfmError)
            {
                context.Alert(lfmError.Message, AlertTypes.Error);
                context.Response.Redirect(context.Request.Path, true);
            }
            catch (Exception e)
            {
                context.Alert(AlertMessages.SystemError, AlertTypes.Error);
                context.Response.Redirect(context.Request.Path, true);
            }
        }
    }
}