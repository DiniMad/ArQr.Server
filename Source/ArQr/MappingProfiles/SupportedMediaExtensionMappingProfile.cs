using AutoMapper;
using Domain;
using Resource.Api.Resources;

namespace ArQr.MappingProfiles
{
    public class SupportedMediaExtensionMappingProfile : Profile
    {
        public SupportedMediaExtensionMappingProfile()
        {
            CreateMap<CreateSupportedMediaExtensionResource, SupportedMediaExtension>();
        }
    }
}