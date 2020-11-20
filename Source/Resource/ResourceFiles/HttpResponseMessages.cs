namespace Resource.ResourceFiles
{
    public record HttpResponseMessages
    {
        public const string Unauthorized          = "Unauthorized";
        public const string UserNotFound          = "UserNotFound";
        public const string IncorrectPassword     = "IncorrectPassword";
        public const string IncorrectRefreshToken = "IncorrectRefreshToken";
    }
}