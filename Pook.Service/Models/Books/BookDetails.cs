using System.Collections.Generic;
using Pook.Data.Entities;
using SBook = Pook.Service.Models.Books.Book;

namespace Pook.Service.Models.Books
{
    public class BookDetails
    {
        public SBook Book { get; set; }

        public IList<Responsability> Responsabilities { get; set; }

        public IList<Note> Notes { get; set; } 
    }
}