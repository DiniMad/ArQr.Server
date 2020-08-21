using System.ComponentModel.DataAnnotations;

namespace ArQr.Controllers.Resources
{
    public class RegisterUserResource
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}