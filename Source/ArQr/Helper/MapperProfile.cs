using AutoMapper;
using Domain;
using Resource.Api.Resources;

namespace ArQr.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRegisterResource, User>();
            CreateMap<User, UserResource>();
            CreateMap<UserRefreshToken, UserRefreshToken>()
                .ForMember(token => token.Id, option => option.Ignore());
        }
    }
}