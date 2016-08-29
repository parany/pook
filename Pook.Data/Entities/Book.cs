using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Book", Schema = "Book")]
    public class Book: Content
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int NumberOfPages { get; set; }

        public Guid? FirmId { get; set; }

        public Firm Firm { get; set; }

        public Guid? EditorId { get; set; }

        public Editor Editor { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Responsability> Responsabilities { get; set; } = new List<Responsability>();

        public virtual ICollection<Progression> Progressions { get; set; } = new List<Progression>();

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>(); 
    }
}