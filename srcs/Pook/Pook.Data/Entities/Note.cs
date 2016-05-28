using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    public class Note : Content
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        public int Page { get; set; }

        public string Description { get; set; }
    }
}
