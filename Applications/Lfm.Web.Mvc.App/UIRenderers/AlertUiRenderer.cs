using Lfm.Web.Mvc.App.SessionAlerts;

namespace Lfm.Web.Mvc.App.UIRenderers
{
    public static class AlertUiRenderer
    {
        public static string GetAlertCssColorClassName(AlertDataModel alert)
        {
            return alert.Type switch
            {
                AlertTypes.Success => "success",
                AlertTypes.Error => "danger",
                AlertTypes.Warning => "warning",
                AlertTypes.Info => "info",
                _ => string.Empty
            };
        }
    }
}