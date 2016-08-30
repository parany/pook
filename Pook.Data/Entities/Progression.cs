using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Progression", Schema = "User")]
    public class Progression : Content
    {
        [Index("IX_Progression", 1, IsUnique = true, Order = 5)]
        public Guid StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Index("IX_Progression", 1, IsUnique = true, Order = 3)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        [Index("IX_Progression", 1, IsUnique = true, Order = 4)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Index("IX_Progression", 1, IsUnique = true, Order = 6)]
        public int? Page { get; set; }
    }
}
