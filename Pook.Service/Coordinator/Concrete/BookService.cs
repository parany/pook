using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Books;
using DBook = Pook.Data.Entities.Book;
using SBook = Pook.Service.Models.Books.Book;
using SNote = Pook.Service.Models.Notes.Note;
using DNote = Pook.Data.Entities.Note;
using SResponsability = Pook.Service.Models.ResponsabilityTypes.Responsability;

namespace Pook.Service.Coordinator.Concrete
{
    public class BookService : IBookService
    {
        #region Private Properties

        private IGenericRepository<DBook> BookRepository { get; }

        private IGenericRepository<Responsability> ResponsabilityRepository { get; }

        private IGenericRepository<DNote> NoteRepository { get; }

        private IGenericRepository<Category> CategoryRepository { get; }

        private IGenericRepository<Firm> FirmRepository { get; }

        private IGenericRepository<Editor> EditorRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<Status> StatusRepository { get; }

        #endregion

        #region Constructors

        public BookService(IGenericRepository<DBook> bookRepository,
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<DNote> noteRepository,
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

            NoteRepository.SetSortExpression(l => l.OrderBy(o => o.Page));
            NoteRepository.AddNavigationProperties(
                u => u.User, 
                u => u.Book
                );

            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author, 
                r => r.ResponsabilityType
                );

            BookRepository.AddNavigationProperties(
                b => b.Category,
                b => b.Editor,
                b => b.Firm
                );
            BookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));

            ProgressionRepository.AddNavigationProperty(b => b.Status);
        }

        #endregion

        #region Public Methods

        public IList<SBook> GetAll()
        {
            var books = BookRepository.GetAll();
            return books.Select(DtoS).ToList();
        }

        public IList<BookList> GetList(string userId)
        {
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
            var books = BookRepository.GetAll();
            var progressions = ProgressionRepository
                .GetList(p => p.UserId == userId)
                .OrderBy(p => p.Date)
                .ToList();
            progressions =
                (from progression in progressions
                 group progression by progression.BookId
                 into g
                 where filter(g.Last())
                 select g.Last()
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

        public BookDetails GetDetails(Guid id)
        {
            SBook book = GetSingle(id);
            var model = new BookDetails
            {
                Book = book,
                Responsabilities = ResponsabilityRepository.GetList(r => r.BookId == book.Id).Select(SResponsability.DtoS).ToList(),
                Notes = NoteRepository.GetList(n => n.BookId == book.Id).Select(SNote.DtoS).ToList()
            };
            return model;
        }

        public BookCreate GetBookCreate()
        {
            return BuidBookCreate();
        }

        public BookCreate GetBookEdit(Guid bookId)
        {
            SBook book = GetSingle(bookId);
            var bookEdit = BuidBookCreate(book.CategoryId, book.EditorId, book.FirmId);
            bookEdit.Book = book;
            return bookEdit;
        }

        public void BookMark(string userId, Guid bookId)
        {
            var bookmarkStatus = StatusRepository.GetSingle(s => s.Title == "Bookmarked");
            var progression = new Progression
            {
                BookId = bookId,
                StatusId = bookmarkStatus.Id,
                UserId = userId,
                Date = DateTime.Now
            };
            ProgressionRepository.Add(progression);
        }

        public void UnBookMark(string userId, Guid bookId)
        {
            var bookmarkStatus = StatusRepository.GetSingle(s => s.Title == "Bookmarked");
            var progression = ProgressionRepository.GetSingle(
                p => p.StatusId == bookmarkStatus.Id
                && p.BookId == bookId
                && p.UserId == userId
                );
            ProgressionRepository.Delete(progression.Id);
        }

        public SBook GetSingle(Guid id)
        {
            var book = BookRepository.GetSingle(id);
            return DtoS(book);
        }

        public void Add(SBook author)
        {
            BookRepository.Add(StoD(author));
        }

        public void Update(SBook author)
        {
            BookRepository.Update(StoD(author));
        }

        public void Delete(Guid id)
        {
            BookRepository.Delete(id);
        }

        #endregion

        #region Private Methods

        private SBook DtoS(DBook book)
        {
            return new SBook
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                CategoryTitle = book.Category?.Title,
                EditorTitle = book.Editor?.Title,
                FirmTitle = book.Firm?.Title,
                NumberOfPages = book.NumberOfPages,
                CategoryId = book.CategoryId,
                EditorId = book.EditorId,
                FirmId = book.FirmId,
                ReleaseDate = book.ReleaseDate
            };
        }

        private DBook StoD(SBook book)
        {
            return new DBook
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                NumberOfPages = book.NumberOfPages,
                ReleaseDate = book.ReleaseDate,
                FirmId = book.FirmId,
                EditorId = book.EditorId,
                CategoryId = book.CategoryId
            };
        }

        private BookCreate BuidBookCreate(Guid? categoryId = null, Guid? editorId = null, Guid? firmId = null)
        {
            var categories = new SelectList(CategoryRepository.GetAll(), "Id", "Title", categoryId);
            var allEditors = EditorRepository.GetAll();
            allEditors.Insert(0, null);
            var editors = new SelectList(allEditors, "Id", "Title", editorId);
            var allFirms = FirmRepository.GetAll();
            allFirms.Insert(0, null);
            var firms = new SelectList(allFirms, "Id", "Title", firmId);
            var bookCreate = new BookCreate
            {
                Categories = categories,
                Editors = editors,
                Firms = firms
            };
            return bookCreate;
        }

        #endregion
    }
}
