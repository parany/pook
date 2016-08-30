using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Books;
using DBook = Pook.Data.Entities.Book;
using SBook = Pook.Service.Models.Books.Book;

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

        public BookService(IGenericRepository<DBook> bookRepository,
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
            return books.Select(Transform).ToList();
        }

        public IList<BookList> GetList(string userId)
        {
            BookRepository.AddNavigationProperty(b => b.Category);
            ProgressionRepository.AddNavigationProperty(b => b.Status);
            var books = BookRepository.GetAll();
            var progressions = ProgressionRepository.GetList(b => b.UserId == userId);
            progressions =
                (from progression in progressions
                 group progression by progression.BookId
                 into g
                 select g.First()
                 ).ToList();
            var bookModels =
                (from book in books
                 let progression = progressions.FirstOrDefault(p => p.BookId == book.Id)
                 select new BookList
                 {
                     Id = book.Id,
                     Status = progression != null ? progression.Status : new Status { Title = "N/A" },
                     Title = book.Title,
                     Category = book.Category.Title,
                     NumberOfPages = book.NumberOfPages,
                     ReleaseDate = book.ReleaseDate
                 }).ToList();
            return bookModels;
        }

        public IList<BookList> GetListByStatus(string userId, Func<Progression, bool> filter)
        {
            BookRepository.AddNavigationProperty(b => b.Category);
            ProgressionRepository.AddNavigationProperty(b => b.Status);
            var books = BookRepository.GetAll();
            var progressions = ProgressionRepository.GetList(p => p.UserId == userId);
            progressions =
                (from progression in progressions
                 group progression by progression.BookId
                 into g
                 where filter(g.First())
                 select g.First()
                 ).ToList();
            var bookModels =
                (from book in books
                 join progression in progressions on book.Id equals progression.BookId
                 select new BookList
                 {
                     Id = book.Id,
                     Status = progression.Status,
                     Title = book.Title,
                     Category = book.Category.Title,
                     NumberOfPages = book.NumberOfPages,
                     ReleaseDate = book.ReleaseDate,
                     Progression = progression
                 }).ToList();
            bookModels = bookModels.OrderByDescending(b => b.Progression.Date).ToList();
            return bookModels;
        }

        public SBook GetSingle(Guid id)
        {
            BookRepository.AddNavigationProperties(
                b => b.Category,
                b => b.Editor,
                b => b.Firm
                );
            var book = BookRepository.GetSingle(id);
            return Transform(book);
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

        private SBook Transform(DBook book)
        {
            return new SBook
            {
                Id = book.Id,
                Title = book.Title,
                CategoryTitle = book.Category?.Title,
                EditorTitle = book.Editor?.Title,
                FirmTitle = book.Firm?.Title,
                NumberOfPages = book.NumberOfPages,
                ReleaseDate = book.ReleaseDate
            };
        }
    }
}
