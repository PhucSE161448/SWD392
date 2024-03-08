using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Nutrition;
using Restaurant.Application.ViewModels.NutritionsDTO;

namespace Restaurant.WebAPI.Controllers.Nutrition
{
    public class NutritionController : BaseController
    {
        private readonly INutritionService _NutritionService;
        public NutritionController(INutritionService NutritionService)
        {
            _NutritionService = NutritionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetNutritionList()
        {
            var result = await _NutritionService.GetAllNutritionAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNutrition(int id)
        {
            var result = await _NutritionService.GetNutritionAsync(id);
            return Ok(result);
        }
        [HttpGet("Ingredient/{ingredientId}")]
        public async Task<IActionResult> GetNutritionByIngredientId(int ingredientId)
        {
            var result = await _NutritionService.GetNutritionByIngredientIdAsync(ingredientId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNutrition([FromForm] NutritionAddDTO createdNutritionDTO)
        {
            string url = "";
            if (createdNutritionDTO.Image != null)
            {
                string fileName = createdNutritionDTO.Name + Path.GetExtension(createdNutritionDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    createdNutritionDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                url = "https://placehold.co/600x400";
            }

            var result = await _NutritionService.CreateNutritionAsync(createdNutritionDTO, url);
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
        public async Task<IActionResult> UpdateNutrition(int id, [FromForm] NutritionUpdateDTO NutritionDTO)
        {
            string url = "";
            if (NutritionDTO.Image != null)
            {
                string fileName = NutritionDTO.Name + Path.GetExtension(NutritionDTO.Image.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    NutritionDTO.Image.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }

            var result = await _NutritionService.UpdateNutritionAsync(id, NutritionDTO, url);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        /* [Authorize(Roles = "admin")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutrition(int id)
        {
            var result = await _NutritionService.DeleteNutritionAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
