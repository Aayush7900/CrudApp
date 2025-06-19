using App.Application.DTO;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id) => Ok(await _productService.GetByIdAsync(id));

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            await _productService.AddAsync(dto);
            return Ok();
        }
        [HttpPut("edit")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateProductDTO dto)
        {
            await _productService.UpdateAsync(dto);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

    }
}
