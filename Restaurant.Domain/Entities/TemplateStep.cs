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
        public int ProductTemplateId { get; set; }
        public string Name { get; set; } = null!;


        public virtual ProductTemplate ProductTemplate { get; set; } = null!;
        public virtual ICollection<IngredientTypeTemplateStep> IngredientTypeTemplateSteps { get; set; }
    }
}
