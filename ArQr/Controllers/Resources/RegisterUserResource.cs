using System.ComponentModel.DataAnnotations;

namespace ArQr.Controllers.Resources
{
    public class RegisterUserResource
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}