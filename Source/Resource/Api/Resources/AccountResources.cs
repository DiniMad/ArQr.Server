namespace Resource.Api.Resources
{
    public record UserRegisterResource(string PhoneNumber, string Password);

    public record UserLoginResource(string PhoneNumber, string Password);
}