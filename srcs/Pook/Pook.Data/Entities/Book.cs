using System;
using System.Collections.Generic;

namespace Pook.Data.Entities
{
    public class Book: Content
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Guid? FirmId { get; set; }

        public Firm Firm { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; } 
    }
}