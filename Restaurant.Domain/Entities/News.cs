using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class News : BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public bool? Status { get; set; }
        public string? Title { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
