using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pook.Service.Models.Books;

namespace Pook.Service.Models.Progressions
{
    public class Progression
    {
        public Guid Id { get; set; }

        public Guid StatusId { get; set; }

        public Guid BookId { get; set; }

        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public int? Page { get; set; }

        public string BookTitle { get; set; }

        public string StatusTitle { get; set; }

        public string UserName { get; set; }

        public static Progression DtoS(Data.Entities.Progression p)
        {
            return new Progression
            {
                Id = p.Id,
                Date = p.Date,
                BookTitle = p.Book.Title,
                StatusTitle = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title,
                UserName = p.User.FullName
            };
        }
    }
}
