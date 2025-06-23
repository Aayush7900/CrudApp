using App.Application.DTO;

namespace App.Application.Interfaces {
    public interface IProductCategory {
        Task<List<ProductCategoryDTO>> GetAllCategoriesAsync();//returns all categories
        Task<ProductCategoryDTO> GetCategoryByNameAsync(string categoryName); //returns a specific category by name
        Task CreateCategoryAsync(CreateProductCategory category);//adds a new category
        Task DeleteCategoryAsync(string categoryName);//deletes a specific category by name
        Task UpdateCategoryAsync(UpdateProductCategoryDTO category);//updates a specific category

    }
}
