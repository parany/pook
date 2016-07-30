using System;
using System.ComponentModel.DataAnnotations;
using Pook.Data.Entities;

namespace Pook.Web.Models
{
    public class BookList
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public int NumberOfPages { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public Status Status { get; set; }

        public Progression Progression { get; set; }

        public bool HasNote { get; set; }
    }
}