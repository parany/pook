using System;
using System.Collections.Generic;
using Pook.Service.Models.Progressions;

namespace Pook.Service.Coordinator.Interface
{
    public interface IProgressionService : IGenericService<Progression>
    {
        ProgressionSearch SortByDate();

        List<Progression> Search(ProgressionSearch search);

        List<ProgressionSection> SortByBook(string userId);

        List<Progression> GetByBook(string userId, Guid bookId);

        ProgressionCreate BuildProgressionCreate(Guid bookId);

        ProgressionCreate BuildProgressionEdit(Guid bookId);
    }
}
