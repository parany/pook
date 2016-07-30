using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

namespace Pook.Web.Controllers
{
    public class StatusController : Controller
    {
        private IGenericRepository<Status> StatusRepository { get; }

        public StatusController(IGenericRepository<Status> statusRepository)
        {
            StatusRepository = statusRepository;
        }

        // GET: Status
        public ActionResult Index()
        {
            return View(StatusRepository.GetAll());
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Status status)
        {
            if (ModelState.IsValid)
            {
                StatusRepository.Add(status);
                return RedirectToAction("Index");
            }

            return View(status);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = StatusRepository.GetSingle(id.Value);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Status status)
        {
            if (ModelState.IsValid)
            {
                StatusRepository.Update(status);
                return RedirectToAction("Index");
            }
            return View(status);
        }
    }
}
