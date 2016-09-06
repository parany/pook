using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using DStatus = Pook.Data.Entities.Status;
using SStatus = Pook.Service.Models.Statuses.Status;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Status")]
    public class StatusController : Controller
    {
        private IGenericRepository<DStatus> StatusRepository { get; }

        private IStatusService StatusService { get; set; }

        public StatusController(IGenericRepository<DStatus> statusRepository, IStatusService statusService)
        {
            StatusService = statusService;
            StatusRepository = statusRepository;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(StatusService.GetAll());
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(SStatus status)
        {
            StatusService.Add(status);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var status = StatusService.GetSingle(id);
            return View(status);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(SStatus status)
        {
            StatusService.Update(status);
            return RedirectToAction("Index");
        }
    }
}
