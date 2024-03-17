using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Ingredient_Type;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;

namespace Restaurant.WebAPI.Controllers.Ingredient_Type
{
    public class IngredientTypeController : BaseController
    {
        private readonly IIngredientTypeService _IngredientTypeService;
        public IngredientTypeController(IIngredientTypeService IngredientTypeService)
        {
            _IngredientTypeService = IngredientTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetIngredientTypeList()
        {
            var result = await _IngredientTypeService.GetAllIngredientTypeAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientType(int id)
        {
            var result = await _IngredientTypeService.GetIngredientTypeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientType([FromBody] AddUpdateIngredientTypeDTO createdIngredientTypeDTO)
        {
            var result = await _IngredientTypeService.CreateIngredientTypeAsync(createdIngredientTypeDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /*[Authorize(Roles = "admin")]*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientType(int id, [FromBody] AddUpdateIngredientTypeDTO IngredientTypeDTO)
        {
            var result = await _IngredientTypeService.UpdateIngredientTypeAsync(id, IngredientTypeDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientType(int id)
        {
            var result = await _IngredientTypeService.DeleteIngredientTypeAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPut("Status/{id}")]
        public async Task<IActionResult> UpdateIsDelete(int id, [FromQuery] bool? isDeleted)
        {
            var result = await _IngredientTypeService.UpdateIsDelete(id, isDeleted);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
