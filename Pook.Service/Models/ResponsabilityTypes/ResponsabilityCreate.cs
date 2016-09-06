using System.Web.Mvc;

namespace Pook.Service.Models.ResponsabilityTypes
{
    public class ResponsabilityCreate
    {
        public SelectList AuthorList { get; set; }

        public SelectList ResponsabilityTypeList { get; set; }

        public Responsability Responsability { get; set; }
    }
}
