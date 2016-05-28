using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Book", Schema = "Book")]
    public class Book: Content
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid? FirmId { get; set; }

        public Firm Firm { get; set; }

        public Guid? EditorId { get; set; }

        public Editor Editor { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

        public virtual ICollection<Progression> Progressions { get; set; } = new List<Progression>();

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>(); 
    }
}