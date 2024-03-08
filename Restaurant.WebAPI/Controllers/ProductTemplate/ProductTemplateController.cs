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
        public async Task<IActionResult> GetProductTemplateList([FromQuery] string? size = null )
        {
            var result = await _ProductTemplateService.GetAllProductTemplateAsync(size);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductTemplate( int id )
        {
            var result = await _ProductTemplateService.GetProductTemplateAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductTemplate([FromForm] ProductTemplateCreateDTO createdProductTemplateDTO)
        {
            string url = "";
            if (createdProductTemplateDTO.Image != null)
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
                url = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                url = "https://placehold.co/600x400";
            }

            var result = await _ProductTemplateService.CreateProductTemplateAsync(createdProductTemplateDTO,url);
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
            string url = "";
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
                url = baseUrl + "/ProductImage/" + fileName;
            }
            
            var result = await _ProductTemplateService.UpdateProductTemplateAsync(id, ProductTemplateDTO,url);
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
