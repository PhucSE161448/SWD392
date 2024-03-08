using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.TemplateSteps
{
    public interface ITemplateStepService
    {
        Task<ServiceResponse<IEnumerable<TemplateStepDTO>>> GetAllTemplateStepAsync(string? size = null);
        Task<ServiceResponse<TemplateStepDTO>> GetTemplateStepAsync(int id);
        Task<ServiceResponse<TemplateStepDTO>> CreateTemplateStepAsync(TemplateStepCreateDTO CreatedTemplateStepDTO);
        Task<ServiceResponse<TemplateStepDTO>> UpdateTemplateStepAsync(int id, TemplateStepUpdateDTO TemplateStepDTO);
        Task<ServiceResponse<bool>> DeleteTemplateStepAsync(int id);
    }
}
