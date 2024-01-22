using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class News : BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
