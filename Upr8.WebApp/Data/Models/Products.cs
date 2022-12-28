using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Upr8.WebApp.Data.Models
{
    public partial class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageMimeType { get; set; }

        public virtual Categories Category { get; set; }
    }
}
