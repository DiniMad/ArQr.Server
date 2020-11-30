using AutoMapper;
using Domain;
using Resource.Api.Resources;

namespace ArQr.MappingProfiles
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Service, ServiceResource>();
            CreateMap<CreateServiceResource, Service>()
                .ForMember(service => service.Active,
                           expression =>
                               expression.MapFrom(_ => true));
        }
    }
}