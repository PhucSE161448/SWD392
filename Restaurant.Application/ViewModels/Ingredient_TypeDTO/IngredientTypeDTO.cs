using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.Ingredient_TypeDTO
{
    public class IngredientTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
