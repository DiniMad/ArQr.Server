using AutoMapper;
using Domain;

namespace ArQr.MappingProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRefreshToken, UserRefreshToken>()
                .ForMember(token => token.UserId, option => option.Ignore());
        }
    }
}