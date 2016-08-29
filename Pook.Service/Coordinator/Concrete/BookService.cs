using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DBook = Pook.Data.Entities.Book;
using SBook = Pook.Service.Models.Book.Book;

namespace Pook.Service.Coordinator.Concrete
{
    public class BookService : IBookService
    {
        private IGenericRepository<DBook> BookRepository { get; }

        private IGenericRepository<Responsability> ResponsabilityRepository { get; }

        private IGenericRepository<Note> NoteRepository { get; }

        private IGenericRepository<Category> CategoryRepository { get; }

        private IGenericRepository<Firm> FirmRepository { get; }

        private IGenericRepository<Editor> EditorRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<Status> StatusRepository { get; }

        public BookService(IGenericRepository<Book> bookRepository,
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<Note> noteRepository,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<Firm> firmRepository,
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<Status> statusRepository,
            IGenericRepository<Editor> editorRepository)
        {
            BookRepository = bookRepository;
            ResponsabilityRepository = responsabilityRepository;
            NoteRepository = noteRepository;
            CategoryRepository = categoryRepository;
            FirmRepository = firmRepository;
            EditorRepository = editorRepository;
            StatusRepository = statusRepository;
            ProgressionRepository = progressionRepository;
        }

        public IList<SBook> GetAll()
        {
            BookRepository.AddNavigationProperties(
                b => b.Category,
                b => b.Editor,
                b => b.Firm
                );
            BookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));
            var books = BookRepository.GetAll();
            return books.Select(b => new SBook
            {
                Id = b.Id,
                Title = b.Title,
                Category = b.Category,
                Editor = b.Editor,
                Firm = b.Firm
            }).ToList();
        }

        public SBook GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(SBook author)
        {
            throw new NotImplementedException();
        }

        public void Update(SBook author)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
