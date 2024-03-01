using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.ProductTemplates;
using Restaurant.Application.ViewModels.ProductTemplateDTO;

namespace Restaurant.WebAPI.Controllers.ProductTemplate
{
    public class ProductTemplateController : BaseController
    {
        private readonly IProductTemplateService _ProductTemplateService;
        public ProductTemplateController(IProductTemplateService ProductTemplateService)
        {
            _ProductTemplateService = ProductTemplateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductTemplateList()
        {
            var result = await _ProductTemplateService.GetAllProductTemplateAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductTemplate([FromForm] ProductTemplateCreateDTO createdProductTemplateDTO)
        {
            if (createdProductTemplateDTO.ImageUrl != null)
            {
                string fileName = createdProductTemplateDTO.Name + Path.GetExtension(createdProductTemplateDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    createdProductTemplateDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                createdProductTemplateDTO.ImageUrl = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                createdProductTemplateDTO.ImageUrl = "https://placehold.co/600x400";
            }

            var result = await _ProductTemplateService.CreateProductTemplateAsync(createdProductTemplateDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

      /*  [Authorize(Roles = "admin")]*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductTemplate(int id, [FromForm] ProductTemplateUpdateDTO ProductTemplateDTO)
        {
            if (ProductTemplateDTO.Image != null)
            {
                string fileName = ProductTemplateDTO.Name + Path.GetExtension(ProductTemplateDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    ProductTemplateDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                ProductTemplateDTO.ImageUrl = baseUrl + "/ProductImage/" + fileName;
            }
            var result = await _ProductTemplateService.UpdateProductTemplateAsync(id, ProductTemplateDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
       /* [Authorize(Roles = "admin")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTemplate(int id)
        {
            var result = await _ProductTemplateService.DeleteProductTemplateAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
