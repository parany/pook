using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.ResponsabilityTypes;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("ResponsabilityType")]
    public class ResponsabilityTypeController : Controller
    {
        private IResponsabilityTypeService ResponsabilityTypeService { get; }

        public ResponsabilityTypeController(IResponsabilityTypeService responsabilityTypeService)
        {
            ResponsabilityTypeService = responsabilityTypeService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(ResponsabilityTypeService.GetAll());
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(ResponsabilityType responsabilityType)
        {
            ResponsabilityTypeService.Add(responsabilityType);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var responsabilityType = ResponsabilityTypeService.GetSingle(id);
            return View(responsabilityType);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(ResponsabilityType responsabilityType)
        {
            ResponsabilityTypeService.Update(responsabilityType);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var responsabilityType = ResponsabilityTypeService.GetSingle(id);
            return View(responsabilityType);
        }

        [Route("Delete/{id}"), HttpPost, ActionName("Delete")]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResponsabilityTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
