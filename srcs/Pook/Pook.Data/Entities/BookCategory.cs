using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    public class BookCategory
    {
        [Key, Column(Order = 0)]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; }

        [Key, Column(Order = 1)]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
