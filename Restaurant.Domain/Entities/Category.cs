using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            ProductTemplates = new HashSet<ProductTemplate>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }


        public virtual ICollection<ProductTemplate> ProductTemplates { get; set; }
    }
}
