using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class OrderDetail : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Note { get; set; } = null!;
        public int Quantity { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Order IdNavigation { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
