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
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ProductTemplate> ProductTemplates { get; set; }
    }
}
