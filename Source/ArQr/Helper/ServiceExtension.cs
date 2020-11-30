using System;
using Domain;
using Domain.Base;

namespace ArQr.Helper
{
    public static class ServiceExtension
    {
        public static BaseDomain<long> CreateDomain(this Service service, long userId)
        {
            return service.ProductType switch
            {
                ProductType.QrCode       => service.CreateQrCode(userId),
                ProductType.MediaContent => service.CreateMediaContent(userId),
                _                        => throw new ArgumentOutOfRangeException(nameof(service.ProductType))
            };
        }
        private static QrCode CreateQrCode(this Service service, long userId)
        {
            var now = DateTimeOffset.UtcNow;
            var qrCode = new QrCode
            {
                Title = "عنوان",
                Description = "توضیحات",
                MaxAllowedViewersCount = service.Constraint,
                CreationDate           = (int) now.ToUnixTimeSeconds(),
                ExpireDate             = (int) now.AddDays(service.ExpireDurationInDays).ToUnixTimeSeconds(),
                OwnerId                = userId
            };
            return qrCode;
        }

        private static MediaContent CreateMediaContent(this Service service, long userId)
        {
            var now = DateTimeOffset.UtcNow;
            var mediaContent = new MediaContent
            {
                MaxSizeInMb  = (byte) service.Constraint,
                CreationDate = (int) now.ToUnixTimeSeconds(),
                ExpireDate   = (int) now.AddDays(service.ExpireDurationInDays).ToUnixTimeSeconds(),
                UserId       = userId,
                ExtensionId = 1
            };
            return mediaContent;
        }
    }
}