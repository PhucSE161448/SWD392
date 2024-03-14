using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class TemplateStepCreateDTO
    {
        public int ProuctTemplateId { get; set; }
        public List<IngredientTypeTemplateSteps> IngredientType { get; set; }
    }
}
