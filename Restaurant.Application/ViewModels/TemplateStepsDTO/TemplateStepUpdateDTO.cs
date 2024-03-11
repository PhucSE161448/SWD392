using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class TemplateStepUpdateDTO
    {
        public List<int> IngredientTypeId { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
