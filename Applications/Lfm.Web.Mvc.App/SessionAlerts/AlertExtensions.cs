using System.Collections.Generic;
using Lfm.Core.Common.Web.Data;
using Lfm.Core.Common.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Lfm.Web.Mvc.App.SessionAlerts
{
    public static class AlertExtensions
    {
        private const string AlertKey = "AlertsDataKey";
        
        private static void AddAlert(this HttpContext context, AlertDataModel alert)
        {
            List<AlertDataModel> sessionAlerts = context.Session.GetObject<List<AlertDataModel>>(AlertKey) ??
                                                 new List<AlertDataModel>();

            sessionAlerts.Add(alert);
            context.Session.SetObject(AlertKey, sessionAlerts);
        }

        public static void Alert(this HttpContext context, string message, AlertTypes type)
        {
            context.AddAlert(new AlertDataModel(message, type));
        }

        public static void AlertError(this Controller controller, string message)
        {
            controller.HttpContext.AddAlert(new AlertDataModel(message, AlertTypes.Error));
        }
        
        public static void AlertWarning(this Controller controller, string message)
        {
            controller.HttpContext.AddAlert(new AlertDataModel(message, AlertTypes.Warning));
        }
        
        public static void AlertSuccess(this Controller controller, string message)
        {
            controller.HttpContext.AddAlert(new AlertDataModel(message, AlertTypes.Success));
        }
        
        public static void AlertInfo(this Controller controller, string message)
        {
            controller.HttpContext.AddAlert(new AlertDataModel(message, AlertTypes.Info));
        }

        public static List<AlertDataModel> RetrieveAlerts(this RazorPage page)
        {
            var alerts = page.Context.Session.GetObject<List<AlertDataModel>>(AlertKey);
            page.Context.Session.Remove(AlertKey);
            return alerts;
        }
        
        public static void AlertErrorAndRedirect(string errorMessage, HttpContext context)
        {
            context.Alert(errorMessage, AlertTypes.Error);
            context.Response.Redirect(Constants.DefaultUrl, true);
        }
    }
}