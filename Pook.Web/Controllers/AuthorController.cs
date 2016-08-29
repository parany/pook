using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Author;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorService AuthorService { get; }

        public AuthorController(IAuthorService authorService)
        {
            AuthorService = authorService;
        }

        [Route("Author")]
        public ActionResult Index()
        {
            return View(AuthorService.GetAll());
        }

        [Route("Author/Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            Author author = AuthorService.GetSingle(id);
            return View(author);
        }

        [Route("Author/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Author/Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Author author)
        {
            AuthorService.Add(author);
            return RedirectToAction("Index");
        }

        [Route("Author/Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            Author author = AuthorService.GetSingle(id);
            return View(author);
        }

        [Route("Author/Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(Author author)
        {
            AuthorService.Update(author);
            return RedirectToAction("Index");
        }
    }
}
