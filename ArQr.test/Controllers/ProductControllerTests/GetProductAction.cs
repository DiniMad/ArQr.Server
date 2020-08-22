using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Models;
using ArQr.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static ArQr.test.UtilityMethods;

namespace ArQr.test.Controllers.ProductControllerTests
{
    public class GetProductAction : ProductControllerTests
    {
        private readonly Mock<IProductRepository> _productRepository;

        public GetProductAction()
        {
            _productRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task GetTheProductBelongsToTheUser_ReturnProduct()
        {
            const string productId = "LITERALLY_ANY_PRODUCT_ID";
            const string userId    = "LITERALLY_ANY_USER_ID";

            var product = new Product
            {
                Id      = productId,
                OwnerId = userId
            };
            _productRepository
                .Setup(repository => repository.GetAsync(productId))
                .ReturnsAsync(product);

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);


            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext(userId)}
            };

            var controllerResult = (OkObjectResult) await controller.GetProduct(productId);
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.NotNull((ProductResource) apiResponse.Data);
        }
        
        [Fact]
        public async Task GetTheProductNotBelongsToTheUser_ReturnNull()
        {
            const string productId = "LITERALLY_ANY_PRODUCT_ID";
            const string ownerId = "LITERALLY_ANY_OWNER_ID";
            const string userId    = "LITERALLY_ANY_USER_ID";

            var product = new Product
            {
                Id      = productId,
                OwnerId = ownerId
            };
            _productRepository
                .Setup(repository => repository.GetAsync(productId))
                .ReturnsAsync(product);

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);


            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext(userId)}
            };

            var controllerResult = (UnauthorizedObjectResult) await controller.GetProduct(productId);
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.Null((ProductResource) apiResponse.Data);
        }
    }
}