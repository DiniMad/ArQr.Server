namespace Blazor.Models
{
    public sealed record ServerEndpoints
    {
        public string Root     { get; set; }
        public string Login    { get; set; }
        public string Register { get; set; }
    }
}