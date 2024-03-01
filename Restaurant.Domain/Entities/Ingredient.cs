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
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long IsDeleted { get; set; }

        public virtual IngredientType IngredientType { get; set; } = null!;
        public virtual ICollection<IngredientProduct> IngredientProducts { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
