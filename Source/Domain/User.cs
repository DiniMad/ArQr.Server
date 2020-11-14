using Domain.Base;

namespace Domain
{
    public class User : BaseDomain
    {
        public string Phone          { get; set; }
        public string PasswordHash   { get; set; }
        public string Email          { get; set; }
        public bool   EmailConfirmed { get; set; }
        public bool   PhoneConfirmed { get; set; }
    }
}