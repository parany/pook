using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Progressions;
using Pook.Web.Filters;
using DProgression = Pook.Data.Entities.Progression;
using SProgression = Pook.Service.Models.Progressions.Progression;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Progression")]
    public class ProgressionController : Controller
    {
        private IGenericRepository<DProgression> ProgressionRepository { get; set; }

        private IGenericRepository<Book> BookRepository { get; set; }

        private IGenericRepository<Status> StatusRepository { get; set; }

        private IProgressionService ProgressionService { get; set; }


        public ProgressionController(
            IGenericRepository<DProgression> progressionRepository,
            IGenericRepository<Book> bookRepository,
            IGenericRepository<Status> statusRepository,
            IProgressionService progressionService
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

            ProgressionService = progressionService;
        }

        [Route("ByDate")]
        public ActionResult ByDate()
        {
            var model = ProgressionService.SortByDate();
            return View(model);
        }

        [Route("Search")]
        public ActionResult Search(ProgressionSearch search)
        {
            var progressions = ProgressionService.Search(search);
            return PartialView(progressions);
        }

        [Route("ByBook")]
        public ActionResult ByBook()
        {
            var userId = User.Identity.GetUserId();
            var progressions = ProgressionService.SortByBook(userId);
            return View(progressions);
        }

        [Route("PageProgress/{userId}/{bookId}")]
        public ActionResult PageProgress(string userId, Guid bookId)
        {
            var progressions = ProgressionService.GetByBook(userId, bookId);
            return View(progressions);
        }

        [HttpGet]
        [Route("Progression/Create/{bookId}")]
        public ActionResult Create(Guid bookId)
        {
            var progressionCreate = ProgressionService.BuildProgressionCreate(bookId);
            return View(progressionCreate);
        }

        [Route("Progression/Create/{bookId}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(SProgression progression)
        {
            progression.UserId = User.Identity.GetUserId();
            ProgressionService.Add(progression);
            return RedirectToAction("PageProgress", new
            {
                userId = progression.UserId,
                bookId = progression.BookId
            });
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var progressionCreate = ProgressionService.BuildProgressionEdit(id);
            return View(progressionCreate);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(SProgression progression)
        {
            progression.UserId = User.Identity.GetUserId();
            ProgressionService.Update(progression);
            return RedirectToAction("PageProgress", new
            {
                userId = progression.UserId,
                bookId = progression.BookId
            });
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var progression = ProgressionService.GetSingle(id);
            return View(progression);
        }

        [Route("Delete/{id}"), HttpPost, ActionName("Delete")]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProgressionService.Delete(id);
            return RedirectToAction("ByBook");
        }
    }
}
