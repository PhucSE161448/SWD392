using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class TemplateStep : BaseEntity
    {
        public TemplateStep()
        {
            IngredientTypeTemplateSteps = new HashSet<IngredientTypeTemplateStep>();
        }

        public int Id { get; set; }
        public int ProuctTemplateId { get; set; }
        public string Name { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ProductTemplate ProuctTemplate { get; set; } = null!;
        public virtual ICollection<IngredientTypeTemplateStep> IngredientTypeTemplateSteps { get; set; }
    }
}
