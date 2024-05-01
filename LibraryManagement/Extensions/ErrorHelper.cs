namespace LibraryManagement.Extensions
{
    public static class ErrorHelper
    {
        public static string GetErrorMessageForCode(string errorCode)
        {
            switch(errorCode)
            {
                case "UserNotFound":
                    return "User not found. Please check your email.";
                case "IncorrectPassword":
                    return "Incorrect password. Please try again.";
                case "LoginFailed":
                    return "Login failed. Please try again later.";
                default:
                    return "An error occurred. Please try again later.";
            }
        }
    }
}
