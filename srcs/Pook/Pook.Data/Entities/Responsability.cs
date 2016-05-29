using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Responsability", Schema = "Book")]
    public class Responsability
    {
        public Guid ResponsabilityId { get; set; }

        public Guid ResponsabilityTypeId { get; set; }

        public virtual ResponsabilityType ResponsabilityType { get; set; }

        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        
    }
}
