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

        private IGenericRepository<Book> BookRepository { get; set; }

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
            throw new NotImplementedException();
        }

        public void Add(SNote entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SNote entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
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

        public IList<NoteByBook> SortByBook()
        {
            throw new NotImplementedException();
        }

        public IList<SNote> GetByBook()
        {
            throw new NotImplementedException();
        }
    }
}
