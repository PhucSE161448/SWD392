using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Store : BaseEntity
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            ProductTemplates = new HashSet<ProductTemplate>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;


        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductTemplate> ProductTemplates { get; set; }
    }
}
