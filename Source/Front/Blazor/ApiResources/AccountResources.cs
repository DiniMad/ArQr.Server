namespace Blazor.ApiResources
{
    public sealed record JwtTokenResource(string Token);

    public sealed record UserRegisterResource(string PhoneNumber, string Password);

    public sealed record UserLoginResource(string PhoneNumber, string Password);

    public sealed record RefreshTokenResource(long UserId, string RefreshToken);
}