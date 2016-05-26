using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    public class BookAuthor
    {
        [Key, Column(Order = 0)]
        public Guid BookId { get; set; }

        [Key, Column(Order = 1)]
        public Guid AuthorId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Author Author { get; set; }

        public string Observation { get; set; }
    }
}
