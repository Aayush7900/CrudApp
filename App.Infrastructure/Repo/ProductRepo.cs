using App.Application;
using App.Application.DTO;
using App.Application.Interfaces;
using App.Core.Entities;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repo
{
    public class ProductRepo : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(CreateProductDTO product) {
            string ImagePath = await SaveFile(product.image) ;
            await _context.Products.AddAsync(new Product {
                Name = product.Name,
                ImagePath = ImagePath,
                Price = product.Price,
                ProductCategoryId = product.ProductCategoryId,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id) {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
                
        }

        public async Task<List<ProductDTO>> GetAllProductAsync() {
            return await _context.Products
                .Select(p => new ProductDTO {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                    ProductCategoryId = p.ProductCategoryId,
                    Price = p.Price,
                }).ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id) {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDTO {
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                    ProductCategoryId = p.ProductCategoryId,
                    Price = p.Price,
                }).FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(UpdateProductDTO product){
            string ImagePath = await SaveFile(product.image);
            await _context.Products
                .Where(p => p.Id == product.Id)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.Name , product.Name).SetProperty(x => x.ImagePath, ImagePath).SetProperty(x => x.Price, product.Price).SetProperty(x => x.ProductCategoryId,product.ProductCategoryId)
                );
        }
        public async Task<string> SaveFile(IFormFile file) {
            if (file == null || file.Length == 0) { return string.Empty; }
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/product/");
            if (!Directory.Exists(uploadFolder)) {
                Directory.CreateDirectory(uploadFolder);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            return "/images/product/" + fileName;
        }
    }
}
