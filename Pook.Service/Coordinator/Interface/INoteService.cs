using System.Collections.Generic;
using Pook.Service.Models.Notes;

namespace Pook.Service.Coordinator.Interface
{
    public interface INoteService : IGenericService<Note>
    {
        NoteSearch SortByDate();

        IList<Note> Search(NoteSearch criteria);

        IList<NoteByBook> SortByBook();

        IList<Note> GetByBook();
    }
}
