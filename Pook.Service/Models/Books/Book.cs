using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pook.Service.Models.Books
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Number Of Pages")]
        public int NumberOfPages { get; set; }

        public string FirmTitle { get; set; }

        public string EditorTitle { get; set; }

        public string CategoryTitle { get; set; }

        [DisplayName("Firm")]
        public Guid? FirmId { get; set; }

        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [DisplayName("Editor")]
        public Guid? EditorId { get; set; }
    }
}