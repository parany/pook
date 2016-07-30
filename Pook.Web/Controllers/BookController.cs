using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Web.Models;

namespace Pook.Web.Controllers
{
    public class BookController : Controller
    {
        private IGenericRepository<Book> BookRepository { get; }

        private IGenericRepository<Responsability> ResponsabilityRepository { get; }

        private IGenericRepository<Note> NoteRepository { get; }

        private IGenericRepository<Category> CategoryRepository { get; }

        private IGenericRepository<Firm> FirmRepository { get; }

        private IGenericRepository<Editor> EditorRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<Status> StatusRepository { get; }


        public BookController(
            IGenericRepository<Book> bookRepository,
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<Note> noteRepository,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<Firm> firmRepository,
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<Status> statusRepository,
            IGenericRepository<Editor> editorRepository
            )
        {
            BookRepository = bookRepository;
            ResponsabilityRepository = responsabilityRepository;
            NoteRepository = noteRepository;
            CategoryRepository = categoryRepository;
            FirmRepository = firmRepository;
            EditorRepository = editorRepository;
            StatusRepository = statusRepository;
            ProgressionRepository = progressionRepository;

            BookRepository.AddNavigationProperties(
                b => b.Category,
                b => b.Editor,
                b => b.Firm
                );
            BookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));
            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author,
                r => r.ResponsabilityType
                );
            NoteRepository.AddNavigationProperty(u => u.User);
            ProgressionRepository.AddNavigationProperty(p => p.Status);
            ProgressionRepository.SetSortExpression(l => l.OrderByDescending(p => p.Date));
        }

        // GET: Book
        public ActionResult Index()
        {
            var books = BookRepository.GetAll();
            books = books.OrderBy(b => b.Title).ToList();
            return View(books.ToList());
        }

        [Route("Book/List")]
        public ViewResult List()
        {
            var books = BookRepository.GetAll();
            var userId = User.Identity.GetUserId();
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
                     Status = progression != null ? progression.Status : new Status { Title = "N/A" } ,
                     Title = book.Title,
                     Category = book.Category.Title,
                     NumberOfPages = book.NumberOfPages,
                     ReleaseDate = book.ReleaseDate
                 }).ToList();
            return View(bookModels);
        }

        [Route("Book/Bookmarked")]
        public ViewResult Bookmarked()
        {
            var books = BookRepository.GetAll();
            var userId = User.Identity.GetUserId();
            var progressions = ProgressionRepository.GetList(p => p.UserId == userId);
            progressions =
                (from progression in progressions
                 group progression by progression.BookId
                 into g
                 where g.First().Status.Title == "Bookmarked"
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
            return View(bookModels);
        }

        [Route("Book/Current")]
        public ViewResult Current()
        {
            var books = BookRepository.GetAll();
            var userId = User.Identity.GetUserId();
            var progressions = ProgressionRepository.GetList(p => p.UserId == userId);
            progressions =
                (from progression in progressions
                 group progression by progression.BookId
                 into g
                 where g.First().Status.Title == "Current"
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
            var bookIds = bookModels.Select(b => b.Id);
            var notes = NoteRepository.GetList(n => bookIds.Contains(n.BookId));
            foreach (var book in bookModels)
            {
                book.HasNote = notes.Any(n => n.BookId == book.Id);
            }
            return View(bookModels);
        }

        [Route("Book/Details/{id}")]
        public ActionResult Details(Guid id)
        {
            Book book = BookRepository.GetSingle(id);

            if (book == null)
                return HttpNotFound();

            var model = new BookDetails
            {
                Book = book,
                Responsabilities = ResponsabilityRepository.GetList(r => r.BookId == book.Id)
            };

            var notes = NoteRepository
                .GetList(n => n.BookId == book.Id)
                .OrderBy(o => o.Page)
                .ToList();
            model.Notes = notes;
            return View(model);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CategoryRepository.GetAll(), "Id", "Title");
            var allEditors = EditorRepository.GetAll();
            allEditors.Insert(0, null);
            ViewBag.EditorId = new SelectList(allEditors, "Id", "Title");
            var allFirms = FirmRepository.GetAll();
            allFirms.Insert(0, null);
            ViewBag.FirmId = new SelectList(allFirms, "Id", "Title");
            return View();
        }

        // POST: Book/Create
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                BookRepository.Add(book);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CategoryRepository.GetAll(), "Id", "Title");
            var allEditors = EditorRepository.GetAll();
            allEditors.Insert(0, null);
            ViewBag.EditorId = new SelectList(allEditors, "Id", "Title");
            var allFirms = FirmRepository.GetAll();
            allFirms.Insert(0, null);
            ViewBag.FirmId = new SelectList(allFirms, "Id", "Title");
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = BookRepository.GetSingle(id.Value);

            if (book == null)
                return HttpNotFound();

            ViewBag.CategoryId = new SelectList(CategoryRepository.GetAll(), "Id", "Title", book.CategoryId);
            var allEditors = EditorRepository.GetAll();
            allEditors.Insert(0, null);
            ViewBag.EditorId = new SelectList(allEditors, "Id", "Title", book.EditorId);
            var allFirms = FirmRepository.GetAll();
            allFirms.Insert(0, null);
            ViewBag.FirmId = new SelectList(allFirms, "Id", "Title", book.FirmId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                BookRepository.Update(book);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CategoryRepository.GetAll(), "Id", "Title", book.CategoryId);
            var allEditors = EditorRepository.GetAll();
            allEditors.Insert(0, null);
            ViewBag.EditorId = new SelectList(allEditors, "Id", "Title", book.EditorId);
            var allFirms = FirmRepository.GetAll();
            allFirms.Insert(0, null);
            ViewBag.FirmId = new SelectList(allFirms, "Id", "Title", book.FirmId);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = BookRepository.GetSingle(id.Value);
            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BookRepository.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Book/Bookmark/5
        public ActionResult Bookmark(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = BookRepository.GetSingle(id.Value);
            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Book/Bookmark/5
        [HttpPost, ActionName("Bookmark")]
        [ValidateAntiForgeryToken]
        public ActionResult BookmarkConfirmed(Guid id)
        {
            var bookmarkStatus = StatusRepository.GetSingle(s => s.Title == "Bookmarked");
            var progression = new Progression
            {
                BookId = id,
                StatusId = bookmarkStatus.Id,
                UserId = User.Identity.GetUserId(),
                Date = DateTime.Now
            };
            ProgressionRepository.Add(progression);
            return RedirectToAction("List");
        }
    }
}
