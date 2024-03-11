using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class IngredientTypeTemplateSteps
    {
        public int IngredientTypeId { get; set; }
        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }
    }
}
