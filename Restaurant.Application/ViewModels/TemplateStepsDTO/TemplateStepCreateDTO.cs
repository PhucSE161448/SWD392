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
        public string Name { get; set; }
        public List<int> IngredientTypeId { get; set; }
        public int Max {  get; set; }
        public int Min { get; set; }
    }
}
