using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Ingredient;
using Restaurant.Application.ViewModels.IngredientsDTO;

namespace Restaurant.WebAPI.Controllers.Ingredient
{
    public class IngredientController : BaseController
    {
        private readonly IIngredientService _IngredientService;
        public IngredientController(IIngredientService IngredientService)
        {
            _IngredientService = IngredientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetIngredientList()
        {
            var result = await _IngredientService.GetAllIngredientAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var result = await _IngredientService.GetIngredientAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromForm] IngredientAddDTO createdIngredientDTO)
        {
            string url = "";
            if (createdIngredientDTO.Image != null)
            {
                string fileName = createdIngredientDTO.Name + Path.GetExtension(createdIngredientDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    createdIngredientDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                url = "https://placehold.co/600x400";
            }

            var result = await _IngredientService.CreateIngredientAsync(createdIngredientDTO, url);
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
        public async Task<IActionResult> UpdateIngredient(int id, [FromForm] IngredientUpdateDTO IngredientDTO)
        {
            string url = "";
            if (IngredientDTO.Image != null)
            {
                string fileName = IngredientDTO.Name + Path.GetExtension(IngredientDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    IngredientDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }

            var result = await _IngredientService.UpdateIngredientAsync(id, IngredientDTO, url);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        /* [Authorize(Roles = "admin")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var result = await _IngredientService.DeleteIngredientAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
