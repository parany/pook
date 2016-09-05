using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Notes;
using DNote = Pook.Data.Entities.Note;
using SNote = Pook.Service.Models.Notes.Note;

namespace Pook.Service.Coordinator.Concrete
{
    public class NoteService : INoteService
    {
        private IGenericRepository<DNote> NoteRepository { get; }

        private IGenericRepository<Book> BookRepository { get; }

        public NoteService(IGenericRepository<DNote> noteRepository, IGenericRepository<Book> bookRepository)
        {
            NoteRepository = noteRepository;
            BookRepository = bookRepository;

            NoteRepository.AddNavigationProperties(
                n => n.Book,
                n => n.User
                );
            NoteRepository.SetSortExpression(l => l.OrderBy(n => n.CreatedOn));
            BookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));
        }

        public IList<SNote> GetAll()
        {
            throw new NotImplementedException();
        }

        public SNote GetSingle(Guid id)
        {
            var note = NoteRepository.GetSingle(id);
            return SNote.DtoS(note);
        }

        public void Add(SNote note)
        {
            var dNote = SNote.StoD(note);
            NoteRepository.Add(dNote);
            note.Id = dNote.Id;
        }

        public void Update(SNote entity)
        {
            NoteRepository.Update(SNote.StoD(entity));
        }

        public void Delete(Guid id)
        {
            NoteRepository.Delete(id);
        }

        public NoteSearch SortByDate()
        {
            var books = BookRepository.GetAll();
            books.Insert(0, null);
            var noteSearch = new NoteSearch
            {
                Notes = NoteRepository.GetAll().Select(SNote.DtoS).ToList(),
                Books = new SelectList(books, "Id", "Title")
            };
            return noteSearch;
        }

        public IList<SNote> Search(NoteSearch search)
        {
            var notes = NoteRepository
                .GetList(n =>
                    (search.BookId == null || n.BookId == search.BookId)
                    && (search.NoteTitle == null || n.Title.Contains(search.NoteTitle))
                    && (search.NoteDescription == null || n.Description.Contains(search.NoteDescription)))
                .Select(SNote.DtoS)
                .ToList();
            return notes;
        }

        public IList<NoteByBook> SortByBook(string userId)
        {
            var notes = NoteRepository.GetList(p => p.UserId == userId);
            var books = notes.Select(p => p.Book).ToList();
            var noteSections =
                (from n in notes
                 orderby n.Book.Title
                 group n by n.Book.Id into g
                 select new NoteByBook
                 {
                     Book = books.First(b => b.Id == g.Key).Title,
                     BookId = g.Key,
                     Notes = g.OrderBy(n => n.Page).Select(SNote.DtoS).ToList()
                 }).ToList();
            return noteSections;
        }

        public IList<SNote> GetByBook(string userId, Guid bookId)
        {
            var notes = NoteRepository
                .GetList(n => n.UserId == userId && n.BookId == bookId)
                .Select(SNote.DtoS)
                .ToList();
            return notes;
        }

        public NoteCreate BuildNoteCreate(Guid? bookId = null)
        {
            var noteCreate = new NoteCreate { Note = new SNote() };
            if (bookId.HasValue)
                noteCreate.Note.BookId = bookId.Value;
            else
                noteCreate.BookSelectList = new SelectList(BookRepository.GetAll(), "Id", "Title");

            return noteCreate;
        }

        public NoteCreate BuildNoteCreate(SNote note)
        {
            var noteCreate = new NoteCreate
            {
                Note = note,
                BookSelectList = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId)
            };
            return noteCreate;
        }
    }
}
