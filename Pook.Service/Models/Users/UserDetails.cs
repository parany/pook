using System.Collections.Generic;
using Pook.Service.Models.Notes;
using Pook.Service.Models.Progressions;

namespace Pook.Service.Models.Users
{
    public class UserDetails
    {
        public Data.Entities.User User { get; set; }

        public List<ProgressionSection> ProgressionSections { get; set; }

        public List<NoteByBook> NoteSections { get; set; }
    }
}