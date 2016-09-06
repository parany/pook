using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Users;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        private IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(UserService.GetAll());
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(string id)
        {
            var userDetails = UserService.GetDetails(id);
            return View(userDetails);
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(User user)
        {
            UserService.Add(user);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}"), HttpGet]
        [NotFound]
        public ActionResult Edit(string id)
        {
            var user = UserService.GetSingle(id);
            return View(user);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(User user)
        {
            UserService.Update(user);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id}"), HttpGet]
        [NotFound]
        public ActionResult Delete(string id)
        {
            User user = UserService.GetSingle(id);
            return View(user);
        }

        [Route("Delete/{id}"), HttpPost]
        [ValidateAntiForgeryToken, ValidateModel]
        public ActionResult DeleteConfirmed(string id)
        {
            UserService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
