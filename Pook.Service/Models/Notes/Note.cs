using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pook.Service.Models.Users;

namespace Pook.Service.Models.Notes
{
    public class Note
    {
        public Guid Id { get; set; }
        
        public int Page { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string UserId { get; set; }

        [DisplayName("Book")]
        public Guid BookId { get; set; }

        public string BookTitle { get; set; }

        public User User { get; set; }

        public static Note DtoS(Data.Entities.Note note)
        {
            return new Note
            {
                Id = note.Id,
                Page = note.Page,
                Title = note.Title,
                Description = note.Description,
                UserId = note.UserId,
                BookId = note.BookId,
                BookTitle = note.Book.Title,
                User = User.DtoS(note.User)
            };
        }

        public static Data.Entities.Note StoD(Note note)
        {
            return new Data.Entities.Note
            {
                Id = note.Id,
                Page = note.Page,
                Title = note.Title,
                Description = note.Description,
                UserId = note.UserId,
                BookId = note.BookId
            };
        }
    }
}
