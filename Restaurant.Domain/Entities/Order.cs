using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; } = null!;
        public double TotalPrice { get; set; }
        public int StoreId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Payment PaymentMethod { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
