using System.Threading.Tasks;
using ArQr.Controllers;
using ArQr.Controllers.Resources;
using ArQr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;

namespace ArQr.test.Controllers.AccountControllerTests
{
    public class RegisterUserAction : AccountControllerTests
    {
        private readonly Mock<IUrlHelper> _urlHelper;

        public RegisterUserAction()
        {
            _urlHelper = new Mock<IUrlHelper>();
        }

        [Fact]
        public async Task OnCallWithValidModel_ReturnUserResourceWithModelData()
        {
            UserManager
                .Setup(manager => manager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _urlHelper
                .Setup(urlHelper => urlHelper.Action(It.IsAny<UrlActionContext>()))
                .Returns(string.Empty);

            var controller = new AccountController(UserManager.Object, Mapper)
            {
                Url = _urlHelper.Object
            };

            var registerUserResource =
                new RegisterUserResource {Email = "LATTERLY_ANY_EMAIL", Password = "LATTERLY_ANY_PASSWORD"};

            var controllerResult = (CreatedResult) await controller.RegisterUser(registerUserResource);
            var apiResponse      = (ApiResponse) controllerResult.Value;
            var userResource     = (UserResource) apiResponse.Data;

            Assert.True(userResource.Email    == registerUserResource.Email &&
                        userResource.UserName == registerUserResource.Email);
        }

        [Theory]
        [InlineData("DuplicateUserName")]
        [InlineData("PasswordRequiresDigit")]
        [InlineData("PasswordRequiresLower")]
        public async Task OnCallWithInvalidModel_ReturnNullAsData(string errorCode)
        {
            UserManager
                .Setup(manager => manager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError {Code = errorCode}));

            var controller = new AccountController(UserManager.Object, Mapper);

            var registerUserResource =
                new RegisterUserResource {Email = "LATTERLY_ANY_EMAIL", Password = "LATTERLY_ANY_PASSWORD"};

            var controllerResult = (BadRequestObjectResult) await controller.RegisterUser(registerUserResource);
            var apiResponse      = (ApiResponse) controllerResult.Value;
            var userResource     = (UserResource) apiResponse.Data;

            Assert.Null(userResource);
        }
    }
}