using System;

namespace ArQr.Models
{
    public class Product
    {
        public string      Id          { get; set; }
        public string      Title       { get; set; }
        public string      Description { get; set; }
        public ProductType Type        { get; set; }
        public string      Content     { get; set; }

        public string          OwnerId { get; set; }
        public ApplicationUser Owner   { get; set; }
    }
}