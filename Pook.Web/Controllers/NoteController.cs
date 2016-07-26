using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

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
        }

        // GET: Note
        public ActionResult Index()
        {
            var notes = NoteRepository.GetAll().OrderByDescending(n => n.CreatedOn);
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

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title");
            return View();
        }

        // POST: Note/Create
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                note.UserId = User.Identity.GetUserId();
                NoteRepository.Add(note);
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", note.BookId);
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
