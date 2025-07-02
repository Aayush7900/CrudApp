using App.Application.DTO;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        public ProductController(IProduct productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllProductAsync());
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id) => Ok(await _productService.GetProductByIdAsync(id));

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]CreateProductDTO dto)
        {
            await _productService.CreateProductAsync(dto);
            return Ok();
        }
        [HttpPut("edit")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateProductDTO dto)
        {
            await _productService.UpdateProductAsync(dto);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
        
    }
}
