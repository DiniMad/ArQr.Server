using System;
using ArQr.Models;
using AutoMapper;
using Domain;

namespace ArQr.MappingProfiles
{
    public class PurchaseMappingProfile : Profile
    {
        private const int ThousandTomanToRialCoefficient = 10000;

        public PurchaseMappingProfile()
        {
            CreateMap<CachePaymentResource, Purchase>()
                .ForMember(purchase => purchase.PaidAmountInThousandToman,
                           expression => expression.MapFrom(resource =>
                                                                resource.PriceInRial / ThousandTomanToRialCoefficient))
                .ForMember(purchase => purchase.Date,
                           expression => expression.MapFrom(_ => DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        }
    }
}