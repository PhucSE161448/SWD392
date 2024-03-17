using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.IngredientsDTO
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Calo { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public IngredientTypeDTO IngredientType { get; set; }
    }
}
