using Microsoft.AspNetCore.Http;

namespace App.Application.DTO {
    public record CreateProductDTO(string Name, decimal Price, int ProductCategoryId, IFormFile file);
}

