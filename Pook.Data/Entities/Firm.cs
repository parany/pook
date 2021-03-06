﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Firm", Schema = "Editor")]
    public class Firm: Content
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; } 
    }
}
