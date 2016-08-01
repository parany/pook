using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class NoteSearch
    {
        public IEnumerable<Note> Notes { get; set; }

        public Guid? BookId { get; set; }

        public SelectList Books { get; set; }

        public string NoteTitle { get; set; }

        public string NoteDescription { get; set; }
    }
}