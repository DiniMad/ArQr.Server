using System;
using AutoMapper;
using Domain;
using Resource.Api.Resources;

namespace ArQr.MappingProfiles
{
    public class QrCodeMappingProfile : Profile
    {
        public QrCodeMappingProfile()
        {
            CreateMap<QrCode, QrCodeResource>();

            CreateMap<QrCode, AuthorizeQrCodeResource>()
                .ForMember(resource => resource.Expired,
                           expression =>
                               expression.MapFrom(code =>
                                                      code.ExpireDate < DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        }
    }
}