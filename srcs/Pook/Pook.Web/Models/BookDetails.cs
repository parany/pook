using System.Collections.Generic;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class BookDetails
    {
        public Book Book { get; set; }

        public IList<Responsability> Responsabilities { get; set; } 

        public IList<Note> Notes { get; set; } 
    }
}