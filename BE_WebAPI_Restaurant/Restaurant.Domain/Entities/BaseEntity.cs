using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModificatedDate { get; set; }

        public int? ModificatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeleteBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
