using System;
using System.Collections.Generic;

namespace Pook.Data.Entities
{
    public class Firm: Content
    {
        public Guid FirmId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
