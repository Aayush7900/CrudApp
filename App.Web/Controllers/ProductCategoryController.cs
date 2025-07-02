using App.Application.DTO;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase {
        private readonly IProductCategory _productCategory;
        public ProductCategoryController(IProductCategory productCategory) {
            _productCategory = productCategory;
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _productCategory.GetAllCategoriesAsync());

        [HttpGet("{Name}")]
        [Authorize]
        public async Task<IActionResult> GetByName(string Name) => Ok(await _productCategory.GetCategoryByNameAsync(Name));

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]CreateProductCategory dto) {
            await _productCategory.CreateCategoryAsync(dto);
            return Ok();
        }
        
        [HttpPut("edit")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateProductCategoryDTO dto) {
            await _productCategory.UpdateCategoryAsync(dto);
            return Ok();
        }
        [HttpDelete("delete/{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id) {
            await _productCategory.DeleteCategoryAsync(Id);
            return Ok();
        }
    }
}
