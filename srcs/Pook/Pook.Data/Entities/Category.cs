using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Category", Schema = "Book")]
    public class Category : Content
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
