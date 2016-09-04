using System.Web.Mvc;

namespace Pook.Service.Models.Notes
{
    public class NoteCreate
    {
        public Note Note { get; set; }

        public SelectList BookSelectList { get; set; }
    }
}
