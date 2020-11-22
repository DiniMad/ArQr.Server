using AutoMapper;
using Domain;

namespace ArQr.MappingProfiles
{
    public class UserRefreshTokenMappingProfile : Profile
    {
        public UserRefreshTokenMappingProfile()
        {
            CreateMap<UserRefreshToken, UserRefreshToken>()
                .ForMember(token => token.UserId, option => option.Ignore());
        }
    }
}