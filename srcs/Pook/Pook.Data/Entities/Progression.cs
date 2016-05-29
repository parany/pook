using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Progression", Schema = "User")]
    public class Progression : Content
    {
        [Key, Column(Order = 0)]
        public Guid StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Key, Column(Order = 1)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        [Key, Column(Order = 2)]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

    }
}
