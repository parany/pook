using System;
using System.ComponentModel;
using Pook.Service.Models.Authors;
using Pook.Service.Models.Users;

namespace Pook.Service.Models.ResponsabilityTypes
{
    public class Responsability
    {
        public Guid Id { get; set; }

        [DisplayName("Responsability Type")]
        public Guid ResponsabilityTypeId { get; set; }

        public ResponsabilityType ResponsabilityType { get; set; }

        [DisplayName("Author")]
        public Guid AuthorId { get; set; }

        public Guid BookId { get; set; }

        public Author Author { get; set; }

        public User User { get; set; }

        public string BookTitle { get; set; }

        public static Responsability DtoS(Data.Entities.Responsability responsability)
        {
            return new Responsability
            {
                Id = responsability.Id,
                ResponsabilityTypeId = responsability.ResponsabilityTypeId,
                AuthorId = responsability.AuthorId,
                BookId = responsability.BookId,
                Author = Author.DtoS(responsability.Author),
                ResponsabilityType = ResponsabilityType.DtoS(responsability.ResponsabilityType),
                BookTitle = responsability.Book != null ? responsability.Book.Title : string.Empty
            };
        }

        public static Data.Entities.Responsability StoD(Responsability responsability)
        {
            return new Data.Entities.Responsability
            {
                Id = responsability.Id,
                ResponsabilityTypeId = responsability.ResponsabilityTypeId,
                AuthorId = responsability.AuthorId,
                BookId = responsability.BookId
            };
        }
    }
}
