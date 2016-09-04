using System.Collections.Generic;
using Pook.Data.Entities;
using DNote = Pook.Service.Models.Notes.Note;
using SBook = Pook.Service.Models.Books.Book;

namespace Pook.Service.Models.Books
{
    public class BookDetails
    {
        public SBook Book { get; set; }

        public IList<Responsability> Responsabilities { get; set; }

        public IList<DNote> Notes { get; set; } 
    }
}