using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using SResponsabilityType = Pook.Service.Models.ResponsabilityTypes.ResponsabilityType;
using DResponsabilityType = Pook.Data.Entities.ResponsabilityType;

namespace Pook.Web.Controllers
{
    [RoutePrefix("ResponsabilityType")]
    public class ResponsabilityTypeController : Controller
    {
        private IGenericRepository<DResponsabilityType> ResponsabilityTypeRepository { get; set; }

        private IResponsabilityTypeService ResponsabilityTypeService { get; set; }

        public ResponsabilityTypeController(
            IGenericRepository<DResponsabilityType> responsabilityTypeRepository,
            IResponsabilityTypeService responsabilityTypeService
            )
        {
            ResponsabilityTypeRepository = responsabilityTypeRepository;
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
        public ActionResult Create(SResponsabilityType responsabilityType)
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
        public ActionResult Edit(SResponsabilityType responsabilityType)
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
