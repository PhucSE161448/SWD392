using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public partial class Session
    {
        public Session()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IngredientId { get; set; }
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
