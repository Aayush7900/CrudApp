using App.Application.DTO;

namespace App.Application.Interfaces{
    public interface IProduct{
        Task<List<ProductDTO>> GetAllProductAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task CreateProductAsync(CreateProductDTO dto);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(UpdateProductDTO product);
    }
}
