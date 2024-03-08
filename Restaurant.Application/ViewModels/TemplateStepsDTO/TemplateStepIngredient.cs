using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class TemplateStepIngredient
    {
        public int TemplateId { get; set; }
        public int IngredientTypeId { get; set; }
        public int Min {  get; set; }
        public int Max { get; set; }
    }
}
