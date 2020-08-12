using ArQr.Controllers.Resources;
using ArQr.Models;
using AutoMapper;

namespace ArQr.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserResource, ApplicationUser>()
                .ForMember(applicationUser => applicationUser.UserName,
                           expression => expression.MapFrom(userResource => userResource.Email));


            CreateMap<ApplicationUser, UserResource>();
        }
    }
}