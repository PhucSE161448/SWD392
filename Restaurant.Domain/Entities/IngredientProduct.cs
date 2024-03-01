using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class IngredientProduct
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
