using AutoMapper;
using Blazor.ApiResources;
using Blazor.Pages.HomePage;

namespace Blazor.MappingProfiles
{
    public class LoginRegisterFormMappingProfile: Profile
    {
        public LoginRegisterFormMappingProfile()
        {
            CreateMap<LoginRegisterModel, UserLoginResource>();
            CreateMap<LoginRegisterModel, UserRegisterResource>();
        }
    }
}