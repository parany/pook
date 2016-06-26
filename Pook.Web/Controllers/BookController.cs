using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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


        public BookController(
            IGenericRepository<Book> bookRepository, 
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<Note> noteRepository,
            IGenericRepository<Category> categoryRepository,
            IGenericRepository<Firm> firmRepository,
            IGenericRepository<Editor> editorRepository
            )
        {
            BookRepository = bookRepository;
            ResponsabilityRepository = responsabilityRepository;
            NoteRepository = noteRepository;
            CategoryRepository = categoryRepository;
            FirmRepository = firmRepository;
            EditorRepository = editorRepository;

            BookRepository.AddNavigationProperties(
                b => b.Category,
                b => b.Editor,
                b => b.Firm
                );
            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author,
                r => r.ResponsabilityType
                );
            NoteRepository.AddNavigationProperty(u => u.User);
        }

        // GET: Book
        public ActionResult Index()
        {
            var books = BookRepository.GetAll();
            return View(books.ToList());
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
        [HttpPost]
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
        [HttpPost]
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
    }
}
