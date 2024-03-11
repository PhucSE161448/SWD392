using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.TemplateSteps
{
    public interface ITemplateStepService
    {
        Task<ServiceResponse<IEnumerable<TemplateStepIngredientDTO>>> GetAllTemplateStepAsync(int? id);
        Task<ServiceResponse<TemplateStepIngredientDTO>> GetTemplateStepAsync(int? id);
        Task<ServiceResponse<TemplateStepDTO>> CreateTemplateStepAsync(TemplateStepCreateDTO CreatedTemplateStepDTO);
        Task<ServiceResponse<TemplateStepDTO>> UpdateTemplateAsync(int id, TemplateStepUpdateDTO templateStep);
        Task<ServiceResponse<bool>> DeleteTemplateStepAsync(int id);
    }
}
