using System;
using System.Collections.Generic;

namespace Pook.Data.Entities
{
    public class Category : Content
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
