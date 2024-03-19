using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            IngredientProducts = new HashSet<IngredientProduct>();
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductTemplateId { get; set; }
        public int? Quantity { get; set; }

        public virtual ProductTemplate ProductTemplate { get; set; } = null!;
        public virtual ICollection<IngredientProduct> IngredientProducts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
