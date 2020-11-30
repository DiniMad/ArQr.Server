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
                                                      code.ExpireDate < DateTimeOffset.UtcNow.ToUnixTimeSeconds()))
                .ForMember(resource => resource.ReachedMaxViews,
                           expression =>
                               expression.MapFrom(code =>
                                                      code.ViewersCount >= code.MaxAllowedViewersCount));

            CreateMap<UpdateQrCodeResource, QrCode>()
                .ForAllMembers(expression =>
                                   expression.Condition((_, _, sourceMember) => sourceMember is not null));
            
            CreateMap<QrCode, UpdateQrCodeResource>()
                .ForAllMembers(expression =>
                                   expression.Condition((_, _, sourceMember) => sourceMember is not null));
        }
    }
}