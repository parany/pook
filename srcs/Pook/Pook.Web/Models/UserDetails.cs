using System.Collections.Generic;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class UserDetails
    {
        public User User { get; set; }

        public List<ProgressionSection> ProgressionSections { get; set; }
    }

    public class ProgressionSection
    {
        public string Book { get; set; }

        public IList<Progression> Progressions { get; set; } 
    }
}