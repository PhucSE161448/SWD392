using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class IngredientType : BaseEntity
    {
        public IngredientType()
        {
            IngredientTypeTemplateSteps = new HashSet<IngredientTypeTemplateStep>();
            Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public virtual ICollection<IngredientTypeTemplateStep> IngredientTypeTemplateSteps { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
