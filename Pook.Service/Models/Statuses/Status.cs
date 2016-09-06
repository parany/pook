using System;

namespace Pook.Service.Models.Statuses
{
    public class Status
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public static Status DtoS(Data.Entities.Status status)
        {
            return new Status
            {
                Id = status.Id,
                Title = status.Title
            };
        }

        public static Data.Entities.Status StoD(Status status)
        {
            return new Data.Entities.Status
            {
                Id = status.Id,
                Title = status.Title
            };
        }
    }
}
