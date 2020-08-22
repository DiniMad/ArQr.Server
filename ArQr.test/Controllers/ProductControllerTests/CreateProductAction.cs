using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Models;
using ArQr.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;
using static ArQr.test.UtilityMethods;

namespace ArQr.test.Controllers.ProductControllerTests
{
    public class CreateProductAction : ProductControllerTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IUrlHelper>         _urlHelper;

        public CreateProductAction()
        {
            _productRepository = new Mock<IProductRepository>();
            _urlHelper         = new Mock<IUrlHelper>();
        }

        [Fact]
        public async Task Always_CallCreateProductOfTheProductRepositoryWithOwnerIdSimilarToHttpContextUserClaim()
        {
            const string userId = "LITERALLY_ANY_USER_ID";

            var productResource = new ProductResource();

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);

            _urlHelper
                .Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                .Returns(string.Empty);

            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext(userId)},
                Url               = _urlHelper.Object
            };

            await controller.CreateProduct(productResource);


            _productRepository.Verify(repository => repository.AddAsync(It.Is<Product>(p => p.OwnerId == userId)),
                                      Times.Once);
        }

        [Fact]
        public async Task Always_CallCompleteMethodOfTheUnitOfWork()
        {
            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);

            _urlHelper
                .Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                .Returns(string.Empty);

            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext()},
                Url               = _urlHelper.Object
            };

            await controller.CreateProduct(new ProductResource());

            UnitOfWork.Verify(unitOfWork => unitOfWork.Complete(), Times.Once);
        }

        [Fact]
        public async Task Always_ReturnProductWithValidData()
        {
            const string      productTitle       = "LITERALLY_ANY_PRODUCT_TITLE";
            const string      productDescription = "LITERALLY_ANY_PRODUCT_DESCRIPTION";
            const ProductType productType        = ProductType.Picture;
            const string      productContent     = "LITERALLY_ANY_PRODUCT_CONTENT";

            var productResource = new ProductResource
            {
                Title       = productTitle,
                Description = productDescription,
                Type        = productType,
                Content     = productContent
            };

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);

            _urlHelper
                .Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                .Returns(string.Empty);

            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext()},
                Url               = _urlHelper.Object
            };

            var controllerResult        = (CreatedResult) await controller.CreateProduct(productResource);
            var apiResponse             = (ApiResponse) controllerResult.Value;
            var responseProductResource = (ProductResource) apiResponse.Data;

            Assert.True(responseProductResource.Title       == productTitle       &&
                        responseProductResource.Description == productDescription &&
                        responseProductResource.Type        == productType        &&
                        responseProductResource.Content     == productContent);
        }
    }
}