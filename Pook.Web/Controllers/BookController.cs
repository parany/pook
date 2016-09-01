using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pook.Data.Entities;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using SBook = Pook.Service.Models.Books.Book;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Book")]
    public class BookController : Controller
    {
        private IBookService BookService { get; set; }


        public BookController(IBookService bookService)
        {
            BookService = bookService;
        }

        [Route("")]
        [Route("~/")]
        public ActionResult Index()
        {
            return View(BookService.GetAll());
        }

        [Route("List")]
        public ViewResult List()
        {
            return View(BookService.GetList(User.Identity.GetUserId()));
        }

        [Route("Bookmarked")]
        public ViewResult Bookmarked()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Bookmarked";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Current")]
        public ViewResult Current()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Current"
                                                         || progression.Status.Title == "StartRead";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Read")]
        public ViewResult Read()
        {
            Func<Progression, bool> filter = progression => progression.Status.Title == "Read";
            var userId = User.Identity.GetUserId();
            var bookModels = BookService.GetListByStatus(userId, filter);
            return View(bookModels);
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            var model = BookService.GetDetails(id);
            return View(model);
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            var book = BookService.GetBookCreate();
            return View(book);
        }

        [Route("Create"), HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false), ValidateModel]
        public ActionResult Create(SBook book)
        {
            BookService.Add(book);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}"), HttpGet]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var bookEdit = BookService.GetBookEdit(id);
            return View(bookEdit);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateAntiForgeryToken, ValidateInput(false), ValidateModel]
        public ActionResult Edit(SBook book)
        {
            BookService.Update(book);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id}"), HttpGet]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            SBook book = BookService.GetSingle(id);
            return View(book);
        }

        [Route("Delete/{id}"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BookService.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("Bookmark/{id}"), HttpGet]
        [NotFound]
        public ActionResult Bookmark(Guid id)
        {
            SBook book = BookService.GetSingle(id);
            return View(book);
        }

        [Route("Bookmark/{id}"), HttpPost]
        [ActionName("Bookmark")]
        [ValidateAntiForgeryToken]
        public ActionResult BookmarkConfirmed(Guid id)
        {
            BookService.BookMark(User.Identity.GetUserId(), id);
            return RedirectToAction("Bookmarked");
        }

        [Route("UnBookmark/{id}"), HttpGet]
        [NotFound]
        public ActionResult UnBookmark(Guid id)
        {
            SBook book = BookService.GetSingle(id);
            return View(book);
        }

        [Route("UnBookmark/{id}"), HttpPost]
        [ActionName("UnBookmark")]
        [ValidateAntiForgeryToken]
        public ActionResult UnBookmarkConfirmed(Guid id)
        {
            BookService.UnBookMark(User.Identity.GetUserId(), id);
            return RedirectToAction("List");
        }
    }
}
