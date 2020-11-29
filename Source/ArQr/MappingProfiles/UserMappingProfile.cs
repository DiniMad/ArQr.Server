using AutoMapper;
using Domain;
using Resource.Api.Resources;

namespace ArQr.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterResource, User>();

            CreateMap<UserUpdateResource, User>()
                .ForMember(user => user.Email,
                           expression =>
                               expression.MapFrom(resource =>
                                                      resource.Email == null ? null : resource.Email.ToLower()))
                .ForMember(user => user.EmailConfirmed,
                           expression =>
                               expression.MapFrom((resource, user) =>
                                                      resource.Email is null && user.EmailConfirmed))
                .ForMember(user => user.PhoneNumberConfirmed,
                           expression =>
                               expression.MapFrom((resource, user) =>
                                                      resource.PhoneNumber is null &&
                                                      user.PhoneNumberConfirmed))
                .ForAllMembers(expression =>
                                   expression.Condition((_, _, sourceMember) => sourceMember is not null));

            CreateMap<User, UserResource>();
        }
    }
}