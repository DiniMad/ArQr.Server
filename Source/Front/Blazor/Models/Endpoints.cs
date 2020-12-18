namespace Blazor.Models
{
    public sealed record Endpoints
    {
        public ClientEndpoints Client { get; set; }
        public ServerEndpoints Server { get; set; }
    }

    public sealed record ClientEndpoints
    {
        public string Login     { get; set; }
        public string Dashboard { get; set; }
    }

    public sealed record ServerEndpoints
    {
        public string Root         { get; set; }
        public string Login        { get; set; }
        public string Register     { get; set; }
        public string RefreshToken { get; set; }
    }
}