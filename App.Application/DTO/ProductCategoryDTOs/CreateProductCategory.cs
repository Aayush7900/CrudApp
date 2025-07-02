using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Application.DTO {
    public class CreateProductCategory {
        [Required]
        public string Name { get; set; } = string.Empty;       
        public IFormFile? ImagePath { get; set; }

    }
}
