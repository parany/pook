using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Responsability", Schema = "Book")]
    public class Responsability : Content
    {
        public Guid ResponsabilityTypeId { get; set; }

        public virtual ResponsabilityType ResponsabilityType { get; set; }

        [Index("IX_Responsability", 1, IsUnique = true, Order = 1)]
        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [Index("IX_Responsability", 1, IsUnique = true, Order = 2)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
