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
    public class NoteController : Controller
    {
        private IGenericRepository<Note> NoteRepository { get; set; }

        private IGenericRepository<Book> BookRepository { get; set; }


        public NoteController(
            IGenericRepository<Note> noteRepository,
            IGenericRepository<Book> bookRepository
            )
        {
            NoteRepository = noteRepository;
            BookRepository = bookRepository;

            NoteRepository.AddNavigationProperties(
                n => n.Book,
                n => n.User
                );
            NoteRepository.SetSortExpression(l => l.OrderBy(n => n.CreatedOn));
            bookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));
        }

        // GET: Note/ByDate
        public ActionResult ByDate()
        {
            NoteRepository.SetSortExpression(l => l.OrderBy(n => n.CreatedOn));
            var books = BookRepository.GetAll();
            books.Insert(0, null);
            var noteSearch = new NoteSearch
            {
                Notes = NoteRepository.GetAll(),
                Books = new SelectList(books, "Id", "Title")
            };
            return View(noteSearch);
        }

        // GET: Note/Search
        [ChildActionOnly]
        public ActionResult Search(NoteSearch search)
        {
            NoteRepository.SetSortExpression(l => l.OrderBy(n => n.CreatedOn));
            var notes = NoteRepository
                .GetList(n =>
                    (search.BookId == null || n.BookId == search.BookId)
                    && (search.NoteTitle == null || n.Title.Contains(search.NoteTitle))
                    && (search.NoteDescription == null || n.Description.Contains(search.NoteDescription)))
                .ToList();

            return PartialView(notes);
        }

        // GET: Note/ByBook
        public ActionResult ByBook()
        {
            var userId = User.Identity.GetUserId();
            var notes = NoteRepository.GetList(p => p.UserId == userId);
            var books = notes.Select(p => p.Book);
            var noteSections =
                (from n in notes
                 orderby n.Book.Title
                 group n by n.Book.Id into g
                 select new NoteSection
                 {
                     Book = books.First(b => b.Id == g.Key).Title,
                     BookId = g.Key,
                     Notes = g.OrderBy(n => n.Page).ToList()
                 }).ToList();

            return View(noteSections);
        }

        [Route("Note/PageNote/{userId}/{bookId}")]
        public ViewResult PageNote(string userId, Guid bookId)
        {
            var notes = NoteRepository.GetList(n => n.UserId == userId && n.BookId == bookId);
            return View(notes);
        }

        // GET: Note/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = NoteRepository.GetSingle(id.Value);
            if (note == null)
                return HttpNotFound();
            
            return View(note);
        }

        [HttpGet]
        [Route("Note/Create/{bookId?}")]
        public ActionResult Create(Guid? bookId)
        {
            var note = new Note();
            if (bookId.HasValue)
                note.BookId = bookId.Value;
            else
                ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title");
            return View(note);
        }

        // POST: Note/Create
        [HttpPost, ValidateInput(false)]
        [Route("Note/Create/{bookId?}")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = User.Identity.GetUserId();
                NoteRepository.Add(note);
                return RedirectToAction("ByBook");
            }
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = NoteRepository.GetSingle(id.Value);
            if (note == null)
                return HttpNotFound();
            
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId);
            return View(note);
        }

        // POST: Note/Edit/5
        [HttpPost, ValidateInput(false) ]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = User.Identity.GetUserId();
                NoteRepository.Update(note);
                return RedirectToAction("ByBook");
            }
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId);
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Note note = NoteRepository.GetSingle(id.Value);
            if (note == null)
                return HttpNotFound();
            
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NoteRepository.Delete(id);
            return RedirectToAction("ByBook");
        }
    }
}
