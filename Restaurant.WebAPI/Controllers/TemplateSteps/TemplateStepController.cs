using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Categories;
using Restaurant.Application.Interfaces.TemplateSteps;
using Restaurant.Application.ViewModels.TemplateStepsDTO;

namespace Restaurant.WebAPI.Controllers.TemplateSteps
{
    public class TemplateStepController : BaseController
    {
        private readonly ITemplateStepService _service;
        public TemplateStepController(ITemplateStepService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetTemplateStepsByProductId([FromQuery] int? productTemplateId = null)
        {
            var result = await _service.GetAllTemplateStepAsync(productTemplateId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductTemplate(int id)
        {
            var result = await _service.GetTemplateStepAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTemplateStep([FromBody] TemplateStepCreateDTO templateStepCreateDTO)
        {

            var result = await _service.CreateTemplateStepAsync(templateStepCreateDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientTypeTemplateSteps(int id, [FromBody] TemplateStepUpdateDTO templateStepUpdateDTO)
        {
                var success = await _service.UpdateTemplateAsync(id, templateStepUpdateDTO);

                if (success.Success)
                {
                    return Ok(success);
                }
                else
                {
                    return BadRequest(success);
                }
            }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTemplate(int id)
        {
            var result = await _service.DeleteTemplateStepAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
