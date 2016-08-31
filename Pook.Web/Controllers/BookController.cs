using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using DBook = Pook.Data.Entities.Book;
using SBook = Pook.Service.Models.Books.Book;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Book")]
    public class BookController : Controller
    {
        private IGenericRepository<DBook> BookRepository { get; }

        private IGenericRepository<Responsability> ResponsabilityRepository { get; }

        private IGenericRepository<Note> NoteRepository { get; }

        private IGenericRepository<Category> CategoryRepository { get; }

        private IGenericRepository<Firm> FirmRepository { get; }

        private IGenericRepository<Editor> EditorRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<Status> StatusRepository { get; }

        private IBookService BookService { get; set; }


        public BookController(
            IGenericRepository<DBook> bookRepository,
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<Note> noteRepository,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<Firm> firmRepository,
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<Status> statusRepository,
            IGenericRepository<Editor> editorRepository,
            IBookService bookService
            )
        {
            BookService = bookService;

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

        [Route("")]
        [Route("~/")]
        public ActionResult Index()
        {
            return View(BookService.GetAll());
        }

        [Route("List")]
        public ViewResult List()
        {
            return View(BookService.GetList(User.Identity.GetUserId()));
        }

        [Route("Bookmarked")]
        public ViewResult Bookmarked()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Bookmarked";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Current")]
        public ViewResult Current()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Current" 
                                                         || progression.Status.Title == "StartRead";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Read")]
        public ViewResult Read()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Read";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            var model = BookService.GetDetails(id);
            return View(model);
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            var book = BookService.GetBookCreate();
            return View(book);
        }

        [Route("Create"), HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false), ValidateModel]
        public ActionResult Create(DBook book)
        {
            BookRepository.Add(book);
            return RedirectToAction("Index");
        }

        // GET: Book/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DBook book = BookRepository.GetSingle(id.Value);

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
        public ActionResult Edit(DBook book)
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

            DBook book = BookRepository.GetSingle(id.Value);
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

            DBook book = BookRepository.GetSingle(id.Value);
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
            return RedirectToAction("Bookmarked");
        }

        // GET: Book/UnBookmark/5
        public ActionResult UnBookmark(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DBook book = BookRepository.GetSingle(id.Value);
            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        // POST: Book/UnBookmark/5
        [HttpPost, ActionName("UnBookmark")]
        [ValidateAntiForgeryToken]
        public ActionResult UnBookmarkConfirmed(Guid id)
        {
            var bookmarkStatus = StatusRepository.GetSingle(s => s.Title == "Bookmarked");
            var progression = ProgressionRepository.GetSingle(
                p => p.StatusId == bookmarkStatus.Id
                && p.BookId == id
                );
            ProgressionRepository.Delete(progression.Id);
            return RedirectToAction("List");
        }
    }
}
