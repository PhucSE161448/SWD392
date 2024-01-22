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
            InverseSizeNavigation = new HashSet<IngredientType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual IngredientType SizeNavigation { get; set; } = null!;
        public virtual ICollection<IngredientTypeTemplateStep> IngredientTypeTemplateSteps { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<IngredientType> InverseSizeNavigation { get; set; }
    }
}
