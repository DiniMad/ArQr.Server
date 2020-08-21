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

        [Fact]
        public async Task OnCallWithUserIdSimilarToHttpContextClaim_ReturnApplicationUser()
        {
            const string userId = "LITERALLY_ANY_USER_ID";
            UserRepository
                .Setup(repository => repository.GetUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var controller = new UserController(UserRepository.Object, Mapper)
            {
                ControllerContext = {HttpContext = CreateHttpContext(userId)}
            };

            var controllerResult = (OkObjectResult) await controller.GetUser(userId);
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.NotNull((UserResource) apiResponse.Data);
        }

        [Fact]
        public async Task OnCallWithUserIdDifferentFromHttpContextClaim_ReturnNull()
        {
            const string httpContextUserId     = "LITERALLY_ANY_USER_ID";
            const string actionParameterUserId = "LITERALLY_ANY_OTHER_USER_ID";
            UserRepository
                .Setup(repository => repository.GetUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var controller = new UserController(UserRepository.Object, Mapper)
            {
                ControllerContext = {HttpContext = CreateHttpContext(httpContextUserId)}
            };

            var controllerResult = (UnauthorizedObjectResult) await controller.GetUser(actionParameterUserId);
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.Null((UserResource) apiResponse.Data);
        }
    }
}