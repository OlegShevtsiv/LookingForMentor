namespace LFM.Core.Common.Data
{
    public static class Messages
    {
        #region CommonOperationMessage
        
        public const string SystemError = "Opps, something goes wrong. \nSystem error.";
        public const string Unauthorized = "You are unauthorized for this action.";
        public const string DataNotFound = "Data not found.";
        public const string InvalidRequest = "Invalid request.";
        public const string RequiredField = "'{0}' is required field.";

        #endregion

        #region Auth

        public const string LoginSuccessful = "Login successful.";
        public const string LoginFailed = "Invalid login attempt.";

        public const string LogoutSuccessful = "Logout successful.";
        public const string LogoutFailed = "Invalid logout attempt.";

        public const string RegistrationSuccessful = "Registration successful.";
        public const string RegistrationFailed = "Invalid registration attempt.";
        
        #endregion

        #region User

        public const string UserWithEmailAlreadyExists = "User with email '{0}' already exists.";
        public const string UserWithPhoneAlreadyExists = "User with phone number '{0}' already exists.";
        public const string UserNotFound = "User not found.";
        public const string UserDoesNotHaveRole = "User doesn't have any role.";
        public const string UserClaimNotFound = "User claim of type '{0}' was not found.";
        public const string InvalidUserClaim = "Invalid User Claim.";

        #endregion

        #region Master data

        public const string TownNotFound = "Town not found.";

        #endregion

        #region Order

        public const string PersonalOrderSuccessful = "You contact request was sent, wait for {0} response.";
        public const string PersonalOrderFailed = "You contact request failed, please contact to administration.";

        #endregion
    }
}