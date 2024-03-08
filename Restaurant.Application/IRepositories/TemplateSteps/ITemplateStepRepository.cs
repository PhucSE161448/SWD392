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
        public Task<(bool success, TemplateStep templateStep)> CreateTemplateAsync(TemplateStepCreateDTO templateStep);
        public Task<IEnumerable<TemplateStepIngredientDTO>> GetTemplateStepsByProductId(int productTemplateId);
    }
}
