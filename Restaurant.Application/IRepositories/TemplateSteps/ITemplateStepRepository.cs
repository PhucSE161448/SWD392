using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.IRepositories.TemplateSteps
{
    public interface ITemplateStepRepository : IGenericRepository<TemplateStep>
    {
        Task<(bool success, TemplateStep templateStep)> CreateTemplateAsync(TemplateStepCreateDTO templateStep);
        Task<(bool success, TemplateStep templateStep)> UpdateTemplateAsync(int id, TemplateStepUpdateDTO templateStep);
        Task<IEnumerable<TemplateStepIngredientDTO>> GetTemplateStepsByProductId(int? productTemplateId);
        Task<TemplateStepIngredientDTO> GetTemplateStepByTemplateStepId(int? templateStepId);

        Task<bool> DeleteTemplateAsync(int TemplateStepId);
    }
}
