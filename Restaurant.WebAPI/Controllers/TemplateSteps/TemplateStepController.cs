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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateStepsByProductId(int id)
        {
            var result = await _service.GetAllTemplateStepAsync(id);
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
    }
}
