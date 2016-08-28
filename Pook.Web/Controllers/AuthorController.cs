using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
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
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                AuthorRepository.Add(author);
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Author author = AuthorRepository.GetSingle(id.Value);
            if (author == null)
                return HttpNotFound();
            
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                AuthorRepository.Update(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Author author = AuthorRepository.GetSingle(id.Value);
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
