namespace Resource.Api.Resources
{
    public sealed record UserResource
    {
        public string Id                   { get; init; }
        public string PhoneNumber          { get; init; }
        public string Email                { get; init; }
        public bool   EmailConfirmed       { get; init; }
        public bool   PhoneNumberConfirmed { get; init; }
    }


    public sealed record UserUpdateResource(string? PhoneNumber, string? Password, string? Email);
}