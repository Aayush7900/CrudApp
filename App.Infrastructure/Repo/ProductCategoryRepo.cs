using App.Application.DTO;
using App.Application.Interfaces;
using App.Core.Entities;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repo {
    public class ProductCategoryRepo : IProductCategory {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepo(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<List<ProductCategoryDTO>> GetAllCategoriesAsync() {
            return await _context.ProductCategory
                .Select(p => new ProductCategoryDTO {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                })
                .ToListAsync();
        }
        public async Task<ProductCategoryDTO> GetCategoryByNameAsync(string categoryName) { 
            return await _context.ProductCategory
                .Where(p => p.Name.ToLower() == categoryName.ToLower())
                .Select(p => new ProductCategoryDTO {
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                })
                .FirstOrDefaultAsync();
        }
        public async Task CreateCategoryAsync(CreateProductCategory dto) {
            
            await _context.ProductCategory.AddAsync(new ProductCategory {
                Name = dto.Name,
                ImagePath = await SaveFile(dto.ImagePath)
            });
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int Id) {
            await _context.ProductCategory
                .Where(p => p.Id == Id)
                .ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

        }
        public async Task UpdateCategoryAsync(UpdateProductCategoryDTO dto) {
            string ImagePath = await SaveFile(dto.ImagePath);
            await _context.ProductCategory
                .Where(p => p.Name.ToLower() == dto.Name.ToLower())
                .ExecuteUpdateAsync( p => p.SetProperty(x => x.Name, dto.NewName)
                                            .SetProperty(x => x.ImagePath, ImagePath));

        }
        public async Task<string> SaveFile(IFormFile file) {
            if (file == null || file.Length == 0) { return string.Empty ; }
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images/productcategory/");
            if (!Directory.Exists(uploadFolder)) {
                Directory.CreateDirectory(uploadFolder);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }
           
            return "/images/productcategory/"+ fileName;
        }

    }
}
