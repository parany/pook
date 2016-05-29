using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Note", Schema = "User")]
    public class Note : Content
    {
        public Guid NoteId { get; set; }

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

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
