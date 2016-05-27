using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Author", Schema = "Book")]
    public class Author : Person
    {
        public Guid AuthorId { get; set; }

        public Guid AuthorRoleId { get; set; }

        public virtual AuthorRole AuthorRole { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
