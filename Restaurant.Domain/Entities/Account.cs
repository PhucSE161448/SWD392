using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Account : BaseEntity
    {
        public Account()
        {
            FeedBacks = new HashSet<FeedBack>();
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
        public string? Avatar { get; set; }

        public string? ConfirmationToken { get; set; }
        public string Gender { get; set; } = null!;
        public bool? IsConfirmed { get; set; }

        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
