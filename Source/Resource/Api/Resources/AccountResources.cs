using System;

namespace Resource.Api.Resources
{
    public sealed record JwtTokenResource(string Token);

    public record UserRegisterResource(string PhoneNumber, string Password);

    public record UserLoginResource(string PhoneNumber, string Password);

    public record RefreshTokenResource(Guid UserId, string RefreshToken);
}