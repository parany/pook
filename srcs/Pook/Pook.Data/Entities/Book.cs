using System;

namespace Pook.Data.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}