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
using Pook.Web.Filters;
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

        [Route("ByBook")]
        public ActionResult ByBook()
        {
            var userId = User.Identity.GetUserId();
            var notesByBook = NoteService.SortByBook(userId);
            return View(notesByBook);
        }

        [Route("PageNote/{userId}/{bookId}")]
        public ViewResult PageNote(string userId, Guid bookId)
        {
            var notes = NoteService.GetByBook(userId, bookId);
            return View(notes);
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            var note = NoteService.GetSingle(id);
            return View(note);
        }

        [HttpGet]
        [Route("Create/{bookId?}")]
        public ActionResult Create(Guid? bookId)
        {
            var noteCreate = NoteService.BuildNoteCreate(bookId);
            return View(noteCreate);
        }

        [Route("Create/{bookId?}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(SNote note)
        {
            note.UserId = User.Identity.GetUserId();
            NoteService.Add(note);
            return RedirectToAction("Details", new { id = note.Id });
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var note = NoteService.GetSingle(id);
            var createNote = NoteService.BuildNoteCreate(note);
            return View(createNote);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(SNote note)
        {
            note.UserId = User.Identity.GetUserId();
            NoteService.Update(note);
            return RedirectToAction("Details", new { id = note.Id });
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var note = NoteService.GetSingle(id);
            return View(note);
        }

        [Route("Delete/{id}"), HttpPost, ActionName("Delete")]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NoteService.Delete(id);
            return RedirectToAction("ByBook");
        }
    }
}
