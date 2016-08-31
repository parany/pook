using System.Collections.Generic;
using System.Web.Mvc;

namespace Pook.Service.Models.Books
{
    public class BookCreate
    {
        public SelectList Categories { get; set; }

        public SelectList Editors { get; set; }

        public SelectList Firms { get; set; }

        public Book Book { get; set; }
    }
}
