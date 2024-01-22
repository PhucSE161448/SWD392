using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class IngredientTypeTemplateStep : BaseEntity
    {
        public int IngredientTypeId { get; set; }
        public int TemplateStepId { get; set; }
        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }

        public virtual IngredientType IngredientType { get; set; } = null!;
        public virtual TemplateStep TemplateStep { get; set; } = null!;
    }
}
