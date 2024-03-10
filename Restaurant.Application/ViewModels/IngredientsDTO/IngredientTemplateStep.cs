using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.IngredientsDTO
{
    public class IngredientTemplateStep
    {
        public IngredientTypeDTO IngredientType { get; set; }
        public List<GetIngredientTemplatStep> items {  get; set; } 
    }
}
