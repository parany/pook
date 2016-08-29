using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using SAuthor = Pook.Service.Models.Author;

namespace Pook.Web.Controllers
{
    public class AuthorController : Controller
    {
        private IGenericRepository<Author> AuthorRepository { get; }

        private IAuthorService AuthorService { get; }

        public AuthorController(
            IGenericRepository<Author> authorRepository,
            IAuthorService authorService
            )
        {
            AuthorService = authorService;
            AuthorRepository = authorRepository;
        }

        // GET: Author
        public ActionResult Index()
        {
            return View(AuthorService.GetAll());
        }

        // GET: Author/Details/5
        [NotFound]
        public ActionResult Details(Guid id)
        {
            SAuthor author = AuthorService.GetSingle(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public ActionResult Create(SAuthor author)
        {
            AuthorService.Add(author);
            return RedirectToAction("Index");
        }

        // GET: Author/Edit/5
        public ActionResult Edit(Guid id)
        {
            SAuthor author = AuthorService.GetSingle(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public ActionResult Edit(SAuthor author)
        {
            AuthorService.Update(author);
            return RedirectToAction("Index");
        }

        // GET: Author/Delete/5
        public ActionResult Delete(Guid id)
        {
            Author author = AuthorRepository.GetSingle(id);
            if (author == null)
                return HttpNotFound();

            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AuthorRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
