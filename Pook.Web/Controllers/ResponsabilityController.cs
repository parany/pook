using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.ResponsabilityTypes;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Responsability")]
    public class ResponsabilityController : Controller
    {
        private IResponsabilityService ResponsabilityService { get; }

        public ResponsabilityController(IResponsabilityService responsabilityService)
        {
            ResponsabilityService = responsabilityService;
        }

        [Route("Create/{bookId?}"), HttpGet]
        public ActionResult Create(Guid? bookId)
        {
            var responsabilityCreate = ResponsabilityService.BuildResponsabilityCreate();
            return View(responsabilityCreate);
        }

        [Route("Create/{bookId}"), HttpPost]
        [ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Guid bookId, Responsability responsability)
        {
            responsability.BookId = bookId;
            ResponsabilityService.Add(responsability);
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var responsability = ResponsabilityService.GetSingle(id);
            return View(responsability);
        }

        [Route("Delete/{id}"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var responsability = ResponsabilityService.GetSingle(id);
            ResponsabilityService.Delete(id);
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }
    }
}
