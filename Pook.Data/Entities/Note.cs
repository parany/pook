using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Note", Schema = "User")]
    public class Note : Content
    {
        [Column(Order = 0)]
        [Index("IX_Note", 1, IsUnique = true, Order = 3)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Column(Order = 1)]
        [Index("IX_Note", 1, IsUnique = true, Order = 4)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        [Index("IX_Note", 1, IsUnique = true, Order = 5)]
        public int Page { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
