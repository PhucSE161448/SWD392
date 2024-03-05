using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.NutritionsDTO
{
    public class NutritionAddDTO
    {
        public int? IngredientId { get; set; }
        public IFormFile? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Vitamin { get; set; }
        public string? HealthValue { get; set; }
        public string? Nutrition1 { get; set; }
    }
}
