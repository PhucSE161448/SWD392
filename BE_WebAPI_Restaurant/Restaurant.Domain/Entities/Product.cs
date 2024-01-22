using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            IngredientProducts = new HashSet<IngredientProduct>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductTemplates = new HashSet<ProductTemplate>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<IngredientProduct> IngredientProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductTemplate> ProductTemplates { get; set; }
    }
}
