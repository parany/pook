using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pook.Service.Models.Progressions
{
    public class Progression
    {
        public Guid Id { get; set; }

        [DisplayName("Status")]
        public Guid StatusId { get; set; }

        [DisplayName("Book")]
        public Guid BookId { get; set; }

        public string UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public int? Page { get; set; }

        [DisplayName("Book")]
        public string BookTitle { get; set; }

        [DisplayName("Status")]
        public string StatusTitle { get; set; }

        [DisplayName("User")]
        public string UserName { get; set; }

        public static Progression DtoS(Data.Entities.Progression p)
        {
            return new Progression
            {
                Id = p.Id,
                Date = p.Date,
                Page = p.Page,
                BookTitle = p.Book.Title,
                StatusTitle = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title,
                UserName = p.User.FullName,
                UserId = p.UserId,
                BookId = p.BookId,
                StatusId = p.StatusId
            };
        }

        public static Data.Entities.Progression StoD(Progression p)
        {
            return new Data.Entities.Progression
            {
                Id = p.Id,
                Date = p.Date,
                Page = p.Page,
                BookId = p.BookId,
                StatusId = p.StatusId,
                UserId = p.UserId
            };
        }
    }
}
