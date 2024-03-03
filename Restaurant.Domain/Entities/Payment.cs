using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Payment : BaseEntity
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PaymentType { get; set; } = null!;
        public bool? IsDelete { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
