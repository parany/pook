using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Statuses;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Status")]
    public class StatusController : Controller
    {
        private IStatusService StatusService { get; set; }

        public StatusController(IStatusService statusService)
        {
            StatusService = statusService;
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
        public ActionResult Create(Status status)
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
        public ActionResult Edit(Status status)
        {
            StatusService.Update(status);
            return RedirectToAction("Index");
        }
    }
}
