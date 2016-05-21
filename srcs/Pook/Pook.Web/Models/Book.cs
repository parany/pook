using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pook.Web.Models
{
    public class Book
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}