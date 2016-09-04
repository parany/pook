using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Concrete;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Notes;
using DNote = Pook.Data.Entities.Note;
using SNote = Pook.Service.Models.Notes.Note;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Note")]
    public class NoteController : Controller
    {
        private IGenericRepository<DNote> NoteRepository { get; set; }

        private IGenericRepository<Book> BookRepository { get; set; }

        private INoteService NoteService { get; set; }

        public NoteController(
            IGenericRepository<DNote> noteRepository,
            IGenericRepository<Book> bookRepository,
            NoteService noteService
            )
        {
            NoteService = noteService;
            NoteRepository = noteRepository;
            BookRepository = bookRepository;

            NoteRepository.AddNavigationProperties(
                n => n.Book,
                n => n.User
                );
            NoteRepository.SetSortExpression(l => l.OrderBy(n => n.CreatedOn));
            BookRepository.SetSortExpression(l => l.OrderBy(b => b.Title));
        }

        [Route("ByDate")]
        public ActionResult ByDate()
        {
            var noteSearch = NoteService.SortByDate();
            return View(noteSearch);
        }

        [Route("Search")]
        public ActionResult Search(NoteSearch search)
        {
            var notes = NoteService.Search(search);
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
                 select new NoteByBook
                 {
                     Book = books.First(b => b.Id == g.Key).Title,
                     BookId = g.Key,
                     Notes = g.OrderBy(n => n.Page).Select(SNote.DtoS).ToList()
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

            DNote note = NoteRepository.GetSingle(id.Value);
            if (note == null)
                return HttpNotFound();
            
            return View(note);
        }

        [HttpGet]
        [Route("Note/Create/{bookId?}")]
        public ActionResult Create(Guid? bookId)
        {
            var note = new DNote();
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
        public ActionResult Create(DNote note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = User.Identity.GetUserId();
                NoteRepository.Add(note);
                return RedirectToAction("Details", new { id = note.Id });
            }
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DNote note = NoteRepository.GetSingle(id.Value);
            if (note == null)
                return HttpNotFound();
            
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId);
            return View(note);
        }

        // POST: Note/Edit/5
        [HttpPost, ValidateInput(false) ]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DNote note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = User.Identity.GetUserId();
                NoteRepository.Update(note);
                return RedirectToAction("Details", new { id = note.Id });
            }
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId);
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DNote note = NoteRepository.GetSingle(id.Value);
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
