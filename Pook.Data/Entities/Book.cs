using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Book", Schema = "Book")]
    public class Book: Content
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
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