using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moq;

namespace ArQr.test.Controllers.AccountControllerTests
{
    public class AccountControllerTests
    {
        protected readonly Mock<UserManager<ApplicationUser>> UserManager;
        protected readonly IMapper                            Mapper;
        protected readonly Mock<IStringLocalizer<Resource>>   Localizer;

        protected AccountControllerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            UserManager =
                new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            var mockMapper = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
            Mapper = mockMapper.CreateMapper();

            Localizer = new Mock<IStringLocalizer<Resource>>();
        }
    }
}