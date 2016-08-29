using System.Collections.Generic;
using Pook.Data.Entities;

namespace Pook.Service.Models.Book
{
    public class BookDetails
    {
        public Pook.Data.Entities.Book Book { get; set; }

        public IList<Responsability> Responsabilities { get; set; } 

        public IList<Note> Notes { get; set; } 
    }
}