using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Book", Schema = "Book")]
    public sealed class Book: Content
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid? FirmId { get; set; }

        public Firm Firm { get; set; }

        public Guid? EditorId { get; set; }

        public Editor Editor { get; set; }

        public ICollection<Category> Categories { get; set; } 

        public ICollection<Author> Authors { get; set; }

        public ICollection<Progression> Progressions { get; set; } 

        public Book()
        {
            Categories = new List<Category>();
        }
    }
}