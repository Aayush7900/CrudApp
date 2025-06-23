using App.Application.DTO;

namespace App.Application.Interfaces{
    public interface IProductService{
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task AddAsync(CreateProductDTO product);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDTO product);
    }
}
