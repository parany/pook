using System;
using System.Collections.Generic;
using Pook.Service.Models.Notes;

namespace Pook.Service.Coordinator.Interface
{
    public interface INoteService : IGenericService<Note>
    {
        NoteSearch SortByDate();

        IList<Note> Search(NoteSearch criteria);

        IList<NoteByBook> SortByBook(string userId);

        IList<Note> GetByBook(string userId, Guid bookId);

        NoteCreate BuildNoteCreate(Guid? bookId = null);

        NoteCreate BuildNoteCreate(Note note);
    }
}
