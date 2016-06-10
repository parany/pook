using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class ProgressionSearch
    {
        public IEnumerable<Progression> Progressions { get; set; }

        public Guid? BookId { get; set; }

        public SelectList Books { get; set; }

        public Guid? StatusId { get; set; }

        public SelectList Statuses { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}