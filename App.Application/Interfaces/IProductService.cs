using App.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces{
    public interface IProductService{
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task AddAsync(CreateProductDTO product);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateProductDTO product);
    }
}
