namespace Blazor.Models
{
    public class ClaimKeys
    {
        public const string Id           = "sub";
        public const string PhoneNumber  = "phone_number";
        public const string RefreshToken = "refresh_token";
        public const string Expire       = "exp";
    }

    public class LocalStorageKeys
    {
        public const string RefreshToken = "refresh_token";
        public const string UserId       = "user_id";
    }
}