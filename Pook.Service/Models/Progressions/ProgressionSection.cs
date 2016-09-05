using System;
using System.Collections.Generic;

namespace Pook.Service.Models.Progressions
{
    public class ProgressionSection
    {
        public string Book { get; set; }

        public Guid BookId { get; set; }

        public IList<Progression> Progressions { get; set; }
    }
}
