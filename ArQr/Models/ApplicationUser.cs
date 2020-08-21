using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ArQr.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Product> Products { get; set; }

        public ApplicationUser()
        {
            Products = new List<Product>();
        }
    }
}