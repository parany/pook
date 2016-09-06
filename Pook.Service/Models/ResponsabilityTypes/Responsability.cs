using System;
using Pook.Service.Models.Authors;
using Pook.Service.Models.Users;

namespace Pook.Service.Models.ResponsabilityTypes
{
    public class Responsability
    {
        public Guid Id { get; set; }

        public Guid ResponsabilityTypeId { get; set; }

        public ResponsabilityType ResponsabilityType { get; set; }

        public Guid AuthorId { get; set; }

        public Guid BookId { get; set; }

        public Author Author { get; set; }

        public User User { get; set; }

        public static Responsability DtoS(Data.Entities.Responsability responsability)
        {
            return new Responsability
            {
                Id = responsability.Id,
                ResponsabilityTypeId = responsability.ResponsabilityTypeId,
                AuthorId = responsability.AuthorId,
                BookId = responsability.BookId,
                Author = Author.DtoS(responsability.Author),
                ResponsabilityType = ResponsabilityType.DtoS(responsability.ResponsabilityType)
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
