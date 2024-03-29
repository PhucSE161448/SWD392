﻿using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class ProductTemplate : BaseEntity
    {
        public ProductTemplate()
        {
            Products = new HashSet<Product>();
            TemplateSteps = new HashSet<TemplateStep>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int? Quantity { get; set; }
        public string Size { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public string? Description { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<TemplateStep> TemplateSteps { get; set; }
    }
}
