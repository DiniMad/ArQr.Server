using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Infrastructure;
using ArQr.Models;
using ArQr.Models.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ArQr.test.Controllers.UserControllerTests
{
    public class UserControllerTests
    {
        protected readonly Mock<IApplicationUserRepository> UserRepository;
        protected readonly IMapper                          Mapper;

        protected UserControllerTests()
        {
            UserRepository = new Mock<IApplicationUserRepository>();
            var mockMapper = new MapperConfiguration(cfg
                                                         => cfg.AddProfile(new AutoMapperProfile()));
            Mapper = mockMapper.CreateMapper();
        }

        protected static HttpContext CreateHttpContext(string userId = "")
        {
            var user = new GenericPrincipal(new ClaimsIdentity(new[]
                                            {
                                                new Claim("type", userId)
                                                {
                                                    Properties = {{"key", "sub"}}
                                                }
                                            }),
                                            null);
            var httpContext = new DefaultHttpContext {User = user};
            return httpContext;
        }
    }
}