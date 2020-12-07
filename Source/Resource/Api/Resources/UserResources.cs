namespace Resource.Api.Resources
{
    public sealed record UserResource
    {
        public string Id                   { get; init; }
        public string PhoneNumber          { get; init; }
        public bool   PhoneNumberConfirmed { get; init; }
    }


    public sealed record UserUpdateResource(string? PhoneNumber, string? Password);

    public sealed record MakeUserAdminResource(long UserId, bool Admin = true);
}