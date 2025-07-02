using Microsoft.AspNetCore.Http;

namespace App.Application.DTO{
    public record UpdateProductDTO{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public IFormFile? image { get; set; }

    }
}
