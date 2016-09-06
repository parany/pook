using System;

namespace Pook.Service.Models.ResponsabilityTypes
{
    public class ResponsabilityType
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Desription { get; set; }

        public static ResponsabilityType DtoS(Data.Entities.ResponsabilityType r)
        {
            return new ResponsabilityType
            {
                Id = r.Id,
                Title = r.Title,
                Desription = r.Desription
            };
        }

        public static Data.Entities.ResponsabilityType StoD(ResponsabilityType r)
        {
            return new Data.Entities.ResponsabilityType
            {
                Id = r.Id,
                Title = r.Title,
                Desription = r.Desription
            };
        }
    }
}
