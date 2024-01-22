using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Account : BaseEntity
    {
        public Account()
        {
            News = new HashSet<News>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? ConfirmationToken { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
