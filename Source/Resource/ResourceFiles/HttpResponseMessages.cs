namespace Resource.ResourceFiles
{
    public record HttpResponseMessages
    {
        public const string Unauthorized                = "Unauthorized";
        public const string UserNotFound                = "UserNotFound";
        public const string IncorrectPassword           = "IncorrectPassword";
        public const string IncorrectRefreshToken       = "IncorrectRefreshToken";
        public const string DuplicatePhoneNumber        = "DuplicatePhoneNumber";
        public const string UnhandledException          = "UnhandledException";
        public const string DuplicatePhoneNumberOrEmail = "DuplicatePhoneNumberOrEmail";
        public const string QrCodeNotFound              = "QrCodeNotFound";
        public const string Done                        = "Done";
        public const string ServiceNotFound             = "ServiceNotFound";
        public const string PaymentAlreadyProcessed     = "ServiceNotFound";
        public const string PaymentAlreadyVerified      = "PaymentAlreadyVerified";
        public const string PaymentExpired              = "PaymentExpired";
        public const string WrongPayment                = "WrongPayment";
        public const string MediaContentNotFound        = "MediaContentNotFound";
        public const string ExtensionNotSupported       = "ExtensionNotSupported";
    }
}