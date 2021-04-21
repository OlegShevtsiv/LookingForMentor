namespace Lfm.Web.Mvc.App.SessionAlerts
{
    public static class AlertMessages
    {
        public const string SystemError = "Opps, something goes wrong. \nSystem error.";
        
        #region Auth

        public const string LoginSuccess = "Login successful.";
        public const string LoginFailed = "Invalid login attempt.";

        public const string LogoutSuccess = "Logout successful.";
        public const string LogoutFailed = "Invalid logout attempt.";

        
        #endregion
    }
}