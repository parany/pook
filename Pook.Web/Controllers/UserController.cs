﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Notes;
using Pook.Service.Models.Progressions;
using Pook.Web.Models;
using DNote = Pook.Data.Entities.Note;
using Progression = Pook.Data.Entities.Progression;
using SProgression = Pook.Service.Models.Progressions.Progression;
using SNote = Pook.Service.Models.Notes.Note;
using DUser = Pook.Data.Entities.User;

namespace Pook.Web.Controllers
{
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        private IUserRepository UserRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<DNote> NoteRepository { get; }

        private IUserService UserService { get; set; }

        public UserController(
            IUserRepository userRepository,
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<DNote> noteRepository,
            IUserService userService
            )
        {
            UserService = userService;

            UserRepository = userRepository;
            ProgressionRepository = progressionRepository;
            NoteRepository = noteRepository;

            ProgressionRepository.SetSortExpression(list => list.OrderBy(p => p.Date));
            ProgressionRepository.AddNavigationProperties(
                p => p.Book,
                p => p.Status
                );
            NoteRepository.SetSortExpression(list => list.OrderBy(n => n.Page));
            NoteRepository.AddNavigationProperty(n => n.Book);
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(UserService.GetAll());
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DUser user = UserRepository.GetSingle(id);
            if (user == null)
                return HttpNotFound();

            var userDetails = new UserDetails { User = user };

            var progressions = ProgressionRepository.GetList(p => p.UserId == user.Id);
            var books = progressions.Select(p => p.Book);
            var progressionSections =
                from p in progressions
                orderby p.Book.Title
                group p by p.Book.Id into g
                select new ProgressionSection
                {
                    Book = books.First(b => b.Id == g.Key).Title,
                    BookId = g.Key,
                    Progressions = g.Select(SProgression.DtoS).ToList()
                };
            userDetails.ProgressionSections = progressionSections.ToList();

            var notes = NoteRepository
                .GetList(p => p.UserId == user.Id);
            var noteSections =
                from p in notes
                group p by p.Book.Title into g
                select new NoteByBook
                {
                    Book = g.Key,
                    Notes = g.Select(SNote.DtoS).ToList()
                };
            userDetails.NoteSections = noteSections.ToList();

            return View(userDetails);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DUser user)
        {
            if (ModelState.IsValid)
            {
                UserRepository.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = UserRepository.GetSingle(id);
            if (user == null)
                return HttpNotFound();
            
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DUser user)
        {
            if (ModelState.IsValid)
            {
                UserRepository.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = UserRepository.GetSingle(id);
            if (user == null)
                return HttpNotFound();
            
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
