namespace Resource.Api.Resources
{
    public record UserResource(string Id,
                               string PhoneNumber,
                               string Email,
                               bool   EmailConfirmed,
                               bool   PhoneNumberConfirmed);
}