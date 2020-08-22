using System.Security.Claims;
using System.Security.Principal;
using ArQr.Data;
using ArQr.Data.UnitOfWork;
using ArQr.Infrastructure;
using ArQr.Localization;
using ArQr.Models.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;

namespace ArQr.test.Controllers.ProductControllerTests
{
    public class ProductControllerTests
    {
        protected readonly Mock<IUnitOfWork>                UnitOfWork;
        protected readonly IMapper                          Mapper;
        protected readonly Mock<IStringLocalizer<Resource>> Localizer;

        public ProductControllerTests()
        {
            UnitOfWork = new Mock<IUnitOfWork>();
            
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            Mapper    = mockMapper.CreateMapper();
            
            Localizer = new Mock<IStringLocalizer<Resource>>();
        }
    }
}