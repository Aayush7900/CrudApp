using App.Application.DTO;
using App.Application.Interfaces;
using App.Core.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        //[Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _productCategory.GetAllCategoriesAsync());

        [HttpGet("{Name}")]
        //[Authorize]
        public async Task<IActionResult> GetByName(string Name) => Ok(await _productCategory.GetCategoryByNameAsync(Name));

        [HttpPost("add/{Name}")]
        //[Authorize]
        public async Task<IActionResult> Create(CreateProductCategory dto) {
            await _productCategory.CreateCategoryAsync(dto);
            return Ok();
        }
        
        [HttpPut("edit")]
        //[Authorize]
        public async Task<IActionResult> Update(UpdateProductCategoryDTO dto) {
            await _productCategory.UpdateCategoryAsync(dto);
            return Ok();
        }
        [HttpDelete("delete/{Name}")]
        //[Authorize]
        public async Task<IActionResult> Delete(string Name) {
            await _productCategory.DeleteCategoryAsync(Name);
            return Ok();
        }
    }
}
