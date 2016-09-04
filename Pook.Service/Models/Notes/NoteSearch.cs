using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Pook.Service.Models.Notes
{
    public class NoteSearch
    {
        public IList<Note> Notes { get; set; }

        public Guid? BookId { get; set; }

        public string NoteTitle { get; set; }

        public string NoteDescription { get; set; }

        public SelectList Books { get; set; }
    }
}