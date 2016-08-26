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
    public class ProgressionController : Controller
    {
        private IGenericRepository<Progression> ProgressionRepository { get; set; }

        private IGenericRepository<Book> BookRepository { get; set; }

        private IGenericRepository<Status> StatusRepository { get; set; }


        public ProgressionController(
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<Book> bookRepository,
            IGenericRepository<Status> statusRepository
            )
        {
            ProgressionRepository = progressionRepository;
            BookRepository = bookRepository;
            StatusRepository = statusRepository;

            ProgressionRepository.AddNavigationProperties(
                p => p.Book,
                p => p.Status,
                p => p.User
                );
            ProgressionRepository.SetSortExpression(l => l.OrderByDescending(p => p.Date));
        }

        // GET: Progression/ByDate
        public ActionResult ByDate()
        {
            var model = new ProgressionSearch();
            var now = DateTime.Now;
            model.EndDate = now;
            model.StartDate = now.AddDays(-90);
            ProgressionRepository.SetSortExpression(p => p.OrderByDescending(r => r.Date));
            var progressions = ProgressionRepository
                .GetList(p => p.Date >= model.StartDate && p.Date <= model.EndDate)
                .ToList();

            var books = BookRepository.GetAll();
            books.Insert(0, null);
            model.Books = new SelectList(books, "Id", "Title");
            var statuses = StatusRepository.GetAll();
            statuses.Insert(0, null);
            model.Statuses = new SelectList(statuses, "Id", "Title");

            progressions = progressions.Select(p => new Progression
            {
                Id = p.Id,
                Date = p.Date,
                Book = p.Book,
                Status = new Status { Title = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title },
                User = p.User
            }).ToList();
            model.Progressions = progressions;

            return View(model);
        }

        // GET: Progression/Search
        public ActionResult Search(ProgressionSearch search)
        {
            ProgressionRepository.SetSortExpression(p => p.OrderByDescending(r => r.Date));
            var progressions = ProgressionRepository
                .GetList(p =>
                    (search.BookId == null || p.BookId == search.BookId)
                    && (search.StatusId == null || p.StatusId == search.StatusId)
                    && (p.Date >= search.StartDate && p.Date <= search.EndDate))
                .ToList();

            progressions = progressions.Select(p => new Progression
            {
                Id = p.Id,
                Date = p.Date,
                Book = p.Book,
                Status = new Status { Title = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title },
                User = p.User
            }).ToList();

            return PartialView(progressions);
        }

        // GET: Progression/ByBook
        public ActionResult ByBook()
        {
            var userId = User.Identity.GetUserId();
            var progressions = ProgressionRepository.GetList(p => p.UserId == userId);
            var books = progressions.Select(p => p.Book);
            var progressionSections =
                (from p in progressions
                 orderby p.Book.Title
                 group p by p.Book.Id into g
                 select new ProgressionSection
                 {
                     Book = books.First(b => b.Id == g.Key).Title,
                     BookId = g.Key,
                     Progressions = g.ToList()
                 }).ToList();

            return View(progressionSections);
        }

        [Route("Progression/PageProgress/{userId}/{bookId}")]
        public ActionResult PageProgress(string userId, Guid bookId)
        {
            ProgressionRepository.SetSortExpression(p => p.OrderByDescending(r => r.Date));
            var progressions = ProgressionRepository
                .GetList(p => p.UserId == userId && p.BookId == bookId)
                .ToList();
            progressions = progressions.Select(p => new Progression
            {
                Id = p.Id,
                Date = p.Date,
                Book = p.Book,
                Status = new Status { Title = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title },
                User = p.User
            }).ToList();
            return View(progressions);
        }

        [HttpGet]
        [Route("Progression/Create/{bookId}")]
        public ActionResult Create(Guid bookId)
        {
            var progression = new Progression
            {
                BookId = bookId,
                Date = DateTime.Now,
            };
            ViewBag.StatusId = new SelectList(StatusRepository.GetAll(), "Id", "Title");
            return View(progression);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Progression/Create/{bookId}")]
        public ActionResult Create(Progression progression, Guid bookId)
        {
            if (ModelState.IsValid)
            {
                progression.UserId = User.Identity.GetUserId();
                ProgressionRepository.Add(progression);
                return RedirectToAction("PageProgress", new
                {
                    userId = progression.UserId,
                    progressionId = progression.Id
                });
            }

            ViewBag.StatusId = new SelectList(StatusRepository.GetAll(), "Id", "Title");
            return View(progression);
        }

        // GET: Progression/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Progression progression = ProgressionRepository.GetSingle(id.Value);
            if (progression == null)
                return HttpNotFound();

            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title", progression.BookId);
            ViewBag.StatusId = new SelectList(StatusRepository.GetAll(), "Id", "Title", progression.StatusId);
            return View(progression);
        }

        // POST: Progression/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Progression progression)
        {
            if (ModelState.IsValid)
            {
                progression.UserId = User.Identity.GetUserId();
                ProgressionRepository.Update(progression);
                return RedirectToAction("PageProgress", new
                {
                    userId = progression.UserId,
                    bookId = progression.BookId
                });
            }
            ViewBag.BookId = new SelectList(BookRepository.GetAll(), "Id", "Title");
            ViewBag.StatusId = new SelectList(StatusRepository.GetAll(), "Id", "Title");
            return View(progression);
        }

        // GET: Progression/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Progression progression = ProgressionRepository.GetSingle(id.Value);
            if (progression == null)
                return HttpNotFound();

            return View(progression);
        }

        // POST: Progression/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var progression = ProgressionRepository.GetSingle(id);
            ProgressionRepository.Delete(id);
            return RedirectToAction("PageProgress", new
            {
                userId = progression.UserId,
                bookId = progression.BookId
            });
        }
    }
}
