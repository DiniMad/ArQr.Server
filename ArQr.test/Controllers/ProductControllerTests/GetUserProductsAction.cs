using System.Collections.Generic;
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
    public class GetUserProductsAction : ProductControllerTests
    {
        private readonly Mock<IProductRepository> _productRepository;

        public GetUserProductsAction()
        {
            _productRepository = new Mock<IProductRepository>();
        }


        [Fact]
        public async Task UserHasProduct_ReturnListOfProductWithAtLeastOnItem()
        {
            const string ownerId = "LITERALLY_ANY_USER_ID";

            var product = new Product {OwnerId = ownerId};

            _productRepository
                .Setup(repository => repository.GetProductsByUserIdAsync(ownerId, It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Product> {product, product, product});

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Products)
                .Returns(_productRepository.Object);


            var controller = new ProductController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext(ownerId)}
            };

            var controllerResult = (OkObjectResult) await controller.GetUserProducts();
            var apiResponse      = (ApiResponse) controllerResult.Value;
            var products         = (IReadOnlyList<ProductResource>) apiResponse.Data;

            Assert.NotEmpty(products);
        }
    }
}