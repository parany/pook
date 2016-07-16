using System;
using System.Collections.Generic;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class UserDetails
    {
        public User User { get; set; }

        public List<ProgressionSection> ProgressionSections { get; set; }

        public List<NoteSection> NoteSections { get; set; }
    }

    public class ProgressionSection
    {
        public string Book { get; set; }

        public Guid BookId { get; set; }

        public IList<Progression> Progressions { get; set; } 
    }

    public class NoteSection
    {
        public string Book { get; set; }

        public IList<Note> Notes { get; set; }
    }
}