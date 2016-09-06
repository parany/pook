using System.Collections.Generic;
using Pook.Service.Models.ResponsabilityTypes;
using Note = Pook.Service.Models.Notes.Note;

namespace Pook.Service.Models.Books
{
    public class BookDetails
    {
        public Book Book { get; set; }

        public IList<Responsability> Responsabilities { get; set; }

        public IList<Note> Notes { get; set; }
    }
}