using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Firms;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Firm")]
    public class FirmController : Controller
    {
        private IFirmService FirmService { get; }

        public FirmController(IFirmService firmService)
        {
            FirmService = firmService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(FirmService.GetAll());
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Firm firm)
        {
            FirmService.Add(firm);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var firm = FirmService.GetSingle(id);
            return View(firm);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(Firm firm)
        {
            FirmService.Update(firm);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var firm = FirmService.GetSingle(id);
            return View(firm);
        }

        [Route("Delete/{id}"), ActionName("Delete"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FirmService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
