using App.Application.DTO;
using App.Application.Interfaces;
using AutoMapper;
using App.Core.Entities;
using App.Core.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task AddAsync(CreateProductDTO product)
        {
            await _productRepository.AddAsync(_mapper.Map<Product>(product));
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            return _mapper.Map<List<ProductDTO>>(await _productRepository.GetAllAsync());
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateProductDTO product)
        {
            await _productRepository.UpdateAsync(_mapper.Map<Product>(product));
        }
    }
}
