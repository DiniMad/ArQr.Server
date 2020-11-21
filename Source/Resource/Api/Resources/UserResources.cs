namespace Resource.Api.Resources
{
    public sealed record UserResource(string Id,
                                      string PhoneNumber,
                                      string Email,
                                      bool   EmailConfirmed,
                                      bool   PhoneNumberConfirmed);

    public sealed record UserUpdateResource(string? PhoneNumber, string? Password, string? Email);
}