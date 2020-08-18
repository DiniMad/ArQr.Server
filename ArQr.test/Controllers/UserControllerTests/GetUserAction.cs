using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ArQr.test.Controllers.UserControllerTests
{
    public class GetUserAction : UserControllerTests
    {
        [Fact]
        public async Task OnCallWithoutParameter_ReturnApplicationUser()
        {
            UserRepository
                .Setup(repository => repository.GetUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var controller = new UserController(UserRepository.Object, Mapper)
            {
                ControllerContext = {HttpContext = CreateHttpContext()}
            };

            var controllerResult = (OkObjectResult) await controller.GetUser();
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.NotNull((UserResource) apiResponse.Data);
        }
    }
}