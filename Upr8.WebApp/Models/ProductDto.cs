using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

using Upr8.WebApp.Data.Models;

namespace Upr8.WebApp.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Име на продукта")]
        [Required(ErrorMessage = "Полето 'Име на продукта' е задълцително")]
        [MaxLength(50, ErrorMessage = "Максималната дължина на полето 'Име на продукта' е 50 символа")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Полето 'Цена' е задълцително")]
        public decimal Price { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Полето 'Категория' е задълцително")]
        public int CategoryId { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Изображение")]
        public IFormFile ImageFile { get; set; }

        public CategoryDto Category { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageMimeType { get; set; }
    }
}
