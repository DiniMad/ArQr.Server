using System.ComponentModel.DataAnnotations;
using ArQr.Localization;
using ArQr.Models;

namespace ArQr.Controllers.Resources
{
    public class ProductResource
    {
        public string Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Title { get; set; }

        [Required]
        [StringLength(32)]
        public string Description { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Required]
        [StringLength(256)]
        public string Content { get; set; }
    }
}