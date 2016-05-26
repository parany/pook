using System;
using System.Collections.Generic;

namespace Pook.Data.Entities
{
    public class Author : Person
    {
        public Guid AuthorId { get; set; }

        public Guid AuthorRoleId { get; set; }

        public virtual AuthorRole AuthorRole { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
