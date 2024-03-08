using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using Restaurant.Application.ViewModels.IngredientsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.TemplateStepsDTO
{
    public class TemplateStepIngredientDTO
    {
        public TemplateStepDTO TemplateStep { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public List<IngredientTypeDTO> IngredientTypes { get; set; }
    }
}
