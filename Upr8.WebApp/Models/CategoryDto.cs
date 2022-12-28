using System.ComponentModel.DataAnnotations;

namespace Upr8.WebApp.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Display(Name = "Име на категория")]
        [Required(ErrorMessage = "Полето 'Име на категория' е задължително")]
        [MaxLength(50, ErrorMessage = "Максималната дължина на полето 'Име на категория' е 50 символа")]
        public string Name { get; set; }
    }
}
