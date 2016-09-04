using System;
using System.Collections.Generic;

namespace Pook.Service.Models.Notes
{
    public class NoteByBook
    {
        public string Book { get; set; }

        public Guid BookId { get; set; }

        public IList<Note> Notes { get; set; }
    }
}
