using System;
using System.Collections.Generic;
using Pook.Data.Entities;
using Pook.Service.Models.Notes;

namespace Pook.Web.Models
{
    public class UserDetails
    {
        public User User { get; set; }

        public List<ProgressionSection> ProgressionSections { get; set; }

        public List<NoteByBook> NoteSections { get; set; }
    }

    public class ProgressionSection
    {
        public string Book { get; set; }

        public Guid BookId { get; set; }

        public IList<Progression> Progressions { get; set; } 
    }
}