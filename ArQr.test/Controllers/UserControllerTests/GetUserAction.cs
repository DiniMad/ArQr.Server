using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Models;
using ArQr.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static ArQr.test.UtilityMethods;

namespace ArQr.test.Controllers.UserControllerTests
{
    public class GetUserAction : UserControllerTests
    {
        private readonly Mock<IApplicationUserRepository> _userRepository;

        public GetUserAction()
        {
            _userRepository = new Mock<IApplicationUserRepository>();
        }

        [Fact]
        public async Task OnCallWithoutParameter_ReturnApplicationUser()
        {
            _userRepository
                .Setup(repository => repository.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Users)
                .Returns(_userRepository.Object);

            var controller = new UserController(UnitOfWork.Object, Mapper, Localizer.Object)
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
            _userRepository
                .Setup(repository => repository.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Users)
                .Returns(_userRepository.Object);

            var controller = new UserController(UnitOfWork.Object, Mapper, Localizer.Object)
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

            _userRepository
                .Setup(repository => repository.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            UnitOfWork
                .Setup(unitOfWork => unitOfWork.Users)
                .Returns(_userRepository.Object);

            var controller = new UserController(UnitOfWork.Object, Mapper, Localizer.Object)
            {
                ControllerContext = {HttpContext = CreateHttpContext(httpContextUserId)}
            };

            var controllerResult = (UnauthorizedObjectResult) await controller.GetUser(actionParameterUserId);
            var apiResponse      = (ApiResponse) controllerResult.Value;

            Assert.Null((UserResource) apiResponse.Data);
        }
    }
}