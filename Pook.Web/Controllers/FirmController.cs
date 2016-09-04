using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using DFirm = Pook.Data.Entities.Firm;
using SFirm = Pook.Service.Models.Firms.Firm;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Firm")]
    public class FirmController : Controller
    {
        private IGenericRepository<DFirm> FirmRepository { get; }

        private IFirmService FirmService { get; set; }

        public FirmController(IGenericRepository<DFirm> firmRepository, IFirmService firmService)
        {
            FirmService = firmService;
            FirmRepository = firmRepository;
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
        public ActionResult Create(SFirm firm)
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
        public ActionResult Edit(SFirm firm)
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
