using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class TemplateStepDTO
    {
        public int Id { get; set; }
        public int ProuctTemplateId { get; set; }
        public string Name { get; set; }
        public IngredientTypeTemplateSteps TemplateStepIngredient { get; set; }
    }
}
