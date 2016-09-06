using System.Web.Mvc;

namespace Pook.Service.Models.Progressions
{
    public class ProgressionCreate
    {
        public Progression Progression { get; set; }

        public SelectList StatusList { get; set; }

        public SelectList BookList { get; set; }
    }
}
