using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            IngredientProducts = new HashSet<IngredientProduct>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int ProductTemplateId { get; set; }

        public virtual ProductTemplate ProductTemplate { get; set; } = null!;
        public virtual ICollection<IngredientProduct> IngredientProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
