namespace ArQr.Interface
{
    public interface IResponseMessages
    {
        public string Unauthorized();
        public string UserNotFound();
        public string IncorrectPassword();
        public string IncorrectRefreshToken();
        public string DuplicatePhoneNumber();
        public string UnhandledException();
        public string DuplicatePhoneNumberOrEmail();
        public string QrCodeNotFound();
        public string Done();
        public string ServiceNotFound();
        public string PaymentAlreadyProcessed();
        public string PaymentAlreadyVerified();
        public string PaymentExpired();
        public string WrongPayment();
        public string MediaContentNotFound();
        public string ExtensionNotSupported();
        public string ViolationOfMediaMaxSize();
        public string MediaExpired();
        public string MediaNotVerified();
        public string EmptyMedia();
        public string SessionExpired();
    }
}