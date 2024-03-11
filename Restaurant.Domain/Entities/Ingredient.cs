using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Ingredient : BaseEntity
    {
        public Ingredient()
        {
            IngredientProducts = new HashSet<IngredientProduct>();
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Calo { get; set; }
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int IngredientTypeId { get; set; }


        public virtual IngredientType IngredientType { get; set; } = null!;
        public virtual Nutrition? Nutrition { get; set; }
        public virtual ICollection<IngredientProduct> IngredientProducts { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
