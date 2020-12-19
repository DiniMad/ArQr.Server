namespace Blazor.Pages.HomePage
{
    public enum LoginRegisterType : byte
    {
        Login,
        Register
    }

    public sealed record LoginRegisterModel(string PhoneNumber = "", string Password = "")
    {
        public string PhoneNumber { get; set; } = PhoneNumber;
        public string Password    { get; set; } = Password;
    }
}