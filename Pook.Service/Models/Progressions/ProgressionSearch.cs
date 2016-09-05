using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pook.Service.Models.Progressions
{
    public class ProgressionSearch
    {
        public IEnumerable<Progression> Progressions { get; set; }

        public Guid? BookId { get; set; }

        public SelectList Books { get; set; }

        public Guid? StatusId { get; set; }

        public SelectList Statuses { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}