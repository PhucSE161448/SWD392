using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public double TotalPrice { get; set; }
        public int StoreId { get; set; }
        public bool? IsDelete { get; set; }
        public int ProductId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Payment PaymentMethod { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
