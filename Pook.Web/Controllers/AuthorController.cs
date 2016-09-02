using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Authors;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Author")]
    public class AuthorController : Controller
    {
        private IAuthorService AuthorService { get; }

        public AuthorController(IAuthorService authorService)
        {
            AuthorService = authorService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(AuthorService.GetAll());
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            Author author = AuthorService.GetSingle(id);
            return View(author);
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Author author)
        {
            AuthorService.Add(author);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            Author author = AuthorService.GetSingle(id);
            return View(author);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(Author author)
        {
            AuthorService.Update(author);
            return RedirectToAction("Index");
        }
    }
}
