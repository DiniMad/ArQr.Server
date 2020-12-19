namespace Blazor.ApiResources
{
    public sealed record UserResource(string Id, string PhoneNumber, bool PhoneNumberConfirmed);

    public sealed record UserUpdateResource(string? PhoneNumber, string? Password);

    public sealed record MakeUserAdminResource(long UserId, bool Admin = true);
}