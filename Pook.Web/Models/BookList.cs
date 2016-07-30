using System;
using System.ComponentModel.DataAnnotations;

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

        public string Status { get; set; }
    }
}