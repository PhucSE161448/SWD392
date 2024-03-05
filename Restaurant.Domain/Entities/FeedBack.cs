using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class FeedBack
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string? Comment { get; set; }

        public virtual Account? Account { get; set; }
    }
}
