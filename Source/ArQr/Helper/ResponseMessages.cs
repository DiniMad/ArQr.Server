using ArQr.Interface;
using Microsoft.Extensions.Localization;
using Resource.ResourceFiles;

namespace ArQr.Helper
{
    public class ResponseMessages : IResponseMessages
    {
        private readonly IStringLocalizer<HttpResponseMessages> _responseMessages;

        public ResponseMessages(IStringLocalizer<HttpResponseMessages> responseMessages)
        {
            _responseMessages = responseMessages;
        }

        public string Unauthorized()                => GetMessage(HttpResponseMessages.Unauthorized);
        public string UserNotFound()                => GetMessage(HttpResponseMessages.UserNotFound);
        public string IncorrectPassword()           => GetMessage(HttpResponseMessages.IncorrectPassword);
        public string IncorrectRefreshToken()       => GetMessage(HttpResponseMessages.IncorrectRefreshToken);
        public string DuplicatePhoneNumber()        => GetMessage(HttpResponseMessages.DuplicatePhoneNumber);
        public string UnhandledException()          => GetMessage(HttpResponseMessages.UnhandledException);
        public string DuplicatePhoneNumberOrEmail() => GetMessage(HttpResponseMessages.DuplicatePhoneNumberOrEmail);
        public string QrCodeNotFound()              => GetMessage(HttpResponseMessages.QrCodeNotFound);
        public string Done()                        => GetMessage(HttpResponseMessages.Done);
        public string ServiceNotFound()             => GetMessage(HttpResponseMessages.ServiceNotFound);
        public string PaymentAlreadyProcessed()     => GetMessage(HttpResponseMessages.PaymentAlreadyProcessed);
        public string PaymentAlreadyVerified()      => GetMessage(HttpResponseMessages.PaymentAlreadyVerified);
        public string PaymentExpired()              => GetMessage(HttpResponseMessages.PaymentExpired);
        public string WrongPayment()                => GetMessage(HttpResponseMessages.WrongPayment);
        public string MediaContentNotFound()        => GetMessage(HttpResponseMessages.MediaContentNotFound);
        public string ExtensionNotSupported()       => GetMessage(HttpResponseMessages.ExtensionNotSupported);
        public string ViolationOfMediaMaxSize()     => GetMessage(HttpResponseMessages.ViolationOfMediaMaxSize);
        public string MediaExpired()                => GetMessage(HttpResponseMessages.MediaExpired);
        public string MediaNotVerified()            => GetMessage(HttpResponseMessages.MediaNotVerified);
        public string EmptyMedia()                  => GetMessage(HttpResponseMessages.EmptyMedia);
        public string SessionExpired()              => GetMessage(HttpResponseMessages.SessionExpired);
        public string DuplicateExtension()          => GetMessage(HttpResponseMessages.DuplicateExtension);
        public string ViolationOfChunkSize          => GetMessage(HttpResponseMessages.ViolationOfChunkSize);

        private string GetMessage(string key) => _responseMessages[key].Value;
    }
}