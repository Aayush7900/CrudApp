using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Application.DTO {
    public class CreateProductDTO{
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int ProductCategoryId { get; set; }
        public IFormFile? image { get; set; }
    };
}

