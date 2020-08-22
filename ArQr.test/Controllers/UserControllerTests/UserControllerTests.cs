using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Data.UnitOfWork;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Models;
using ArQr.Models.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace ArQr.test.Controllers.UserControllerTests
{
    public class UserControllerTests
    {
        protected readonly Mock<IUnitOfWork>                UnitOfWork;
        protected readonly IMapper                          Mapper;
        protected readonly Mock<IStringLocalizer<Resource>> Localizer;

        protected UserControllerTests()
        {
            UnitOfWork = new Mock<IUnitOfWork>();
            
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            Mapper = mockMapper.CreateMapper();

            Localizer = new Mock<IStringLocalizer<Resource>>();
        }
    }
}