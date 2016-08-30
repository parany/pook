using System;
using System.ComponentModel.DataAnnotations;

namespace Pook.Service.Models.Books
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime ReleaseDate { get; set; }

        public int NumberOfPages { get; set; }

        public string FirmTitle { get; set; }

        public string EditorTitle { get; set; }

        public string CategoryTitle { get; set; }
    }
}