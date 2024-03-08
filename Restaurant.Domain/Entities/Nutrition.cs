using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Nutrition : BaseEntity
    {
        public int Id { get; set; }
        public int? IngredientId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Vitamin { get; set; }
        public string? HealthValue { get; set; }
        public string? Nutrition1 { get; set; }

        public virtual Ingredient? Ingredient { get; set; }
    }
}
