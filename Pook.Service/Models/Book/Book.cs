using System;
using System.ComponentModel.DataAnnotations;
using Pook.Data.Entities;

namespace Pook.Service.Models.Book
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

        public Guid? FirmId { get; set; }

        public Firm Firm { get; set; }

        public Guid? EditorId { get; set; }

        public Editor Editor { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}