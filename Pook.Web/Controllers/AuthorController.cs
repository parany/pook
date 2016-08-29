using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using SAuthor = Pook.Service.Models.Author;

namespace Pook.Web.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService AuthorService { get; }

        public AuthorController(IAuthorService authorService)
        {
            AuthorService = authorService;
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
        [NotFound]
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
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            SAuthor author = AuthorService.GetSingle(id);
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AuthorService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
