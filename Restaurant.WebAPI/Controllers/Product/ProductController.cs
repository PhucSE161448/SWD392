using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IClaimsService _claimsService;
        public ProductController(IProductService productService, IClaimsService claimsService)
        {
            _productService = productService;
            _claimsService = claimsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var name = _claimsService.GetCurrentUserId;
            var id = _claimsService.GetCurrentUser;
            var result = await _productService.GetAllProductAsync(name);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetProductAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreatedProductDTO createdProductDTO)
        {
            var result = await _productService.CreateProductAsync(createdProductDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return RedirectToAction(nameof(GetProductList));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDTO)
        {
            var result = await _productService.UpdateProductAsync(id, productDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        //[Authorize(Roles = "admin")]
        //[HttpGet("{name}")]
        //public async Task<IActionResult> SearchProductByName(string name)
        //{
        //    var result = await _productService.SearchProductByNameAsync(name);
        //    if (!result.Success)
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(result);
        //}
        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //public async Task<IActionResult> GetSortedProductAsync()
        //{
        //    var result = await _productService.GetSortedProductAsync();
        //    if(!result.Success)
        //    {
        //        return NotFound(result);
        //    }
        //    else
        //    {
        //        return Ok(result);
        //    }
        //}
    }
}
