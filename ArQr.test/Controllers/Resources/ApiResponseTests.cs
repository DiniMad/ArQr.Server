using ArQr.Controllers.Resources;
using Xunit;

namespace ArQr.test.Controllers.Resources
{
    public class ApiResponseTests
    {
        [Fact]
        public void Ok_Always_ShouldReturnModelWithSuccessSetToTrue()
        {
            var okObjectResult = ApiResponse.Ok(null);
            var apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.True(apiResponse.Success);
        }

        [Fact]
        public void Ok_Always_ShouldReturnModelWithStatusSetTo200()
        {
            const int status         = 200;
            var       okObjectResult = ApiResponse.Ok(null);
            var       apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.Equal(apiResponse.Status, status);
        }

        [Fact]
        public void Created_Always_ShouldReturnModelWithSuccessSetToTrue()
        {
            var createdResult = ApiResponse.Created(string.Empty, null);
            var apiResponse   = (ApiResponse) createdResult.Value;

            Assert.True(apiResponse.Success);
        }

        [Fact]
        public void Created_Always_ShouldReturnModelWithStatusSetTo201()
        {
            const int status         = 201;
            var       okObjectResult = ApiResponse.Created(string.Empty, null);
            var       apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.Equal(apiResponse.Status, status);
        }

        [Fact]
        public void BadRequest_Always_ShouldReturnModelWithSuccessSetToFalse()
        {
            var createdResult = ApiResponse.BadRequest(string.Empty);
            var apiResponse   = (ApiResponse) createdResult.Value;

            Assert.False(apiResponse.Success);
        }

        [Fact]
        public void BadRequest_Always_ShouldReturnModelWithStatusSetTo400()
        {
            const int status         = 400;
            var       okObjectResult = ApiResponse.BadRequest(string.Empty);
            var       apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.Equal(apiResponse.Status, status);
        }

        [Fact]
        public void UnAuthorize_Always_ShouldReturnModelWithSuccessSetToFalse()
        {
            var createdResult = ApiResponse.UnAuthorize(string.Empty);
            var apiResponse   = (ApiResponse) createdResult.Value;

            Assert.False(apiResponse.Success);
        }

        [Fact]
        public void UnAuthorize_Always_ShouldReturnModelWithStatusSetTo400()
        {
            const int status         = 401;
            var       okObjectResult = ApiResponse.UnAuthorize(string.Empty);
            var       apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.Equal(apiResponse.Status, status);
        }

        [Fact]
        public void NotFound_Always_ShouldReturnModelWithSuccessSetToFalse()
        {
            var createdResult = ApiResponse.NotFound(string.Empty);
            var apiResponse   = (ApiResponse) createdResult.Value;

            Assert.False(apiResponse.Success);
        }

        [Fact]
        public void NotFound_Always_ShouldReturnModelWithStatusSetTo404()
        {
            const int status         = 404;
            var       okObjectResult = ApiResponse.NotFound(string.Empty);
            var       apiResponse    = (ApiResponse) okObjectResult.Value;

            Assert.Equal(apiResponse.Status, status);
        }

        [Fact]
        public void ServerError_Always_ShouldReturnModelWithSuccessSetToFalse()
        {
            var apiResponse = ApiResponse.ServerError(string.Empty);

            Assert.False(apiResponse.Success);
        }

        [Fact]
        public void ServerError_Always_ShouldReturnModelWithStatusSetTo500()
        {
            const int status         = 500;
            var apiResponse = ApiResponse.ServerError(string.Empty);

            Assert.Equal(apiResponse.Status, status);
        }
    }
}