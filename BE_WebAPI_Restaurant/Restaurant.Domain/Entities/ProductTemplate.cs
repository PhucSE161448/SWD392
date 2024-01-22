﻿using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class ProductTemplate : BaseEntity
    {
        public ProductTemplate()
        {
            TemplateSteps = new HashSet<TemplateStep>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public string Size { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; } = null!;
        public int StoreId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<TemplateStep> TemplateSteps { get; set; }
    }
}
