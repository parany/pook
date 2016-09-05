using System;
using System.Collections.Generic;
using Pook.Data.Entities;
using Pook.Service.Models.Notes;
using Pook.Service.Models.Progressions;

namespace Pook.Web.Models
{
    public class UserDetails
    {
        public User User { get; set; }

        public List<ProgressionSection> ProgressionSections { get; set; }

        public List<NoteByBook> NoteSections { get; set; }
    }
}