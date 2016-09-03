using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Firm")]
    public class FirmController : Controller
    {
        private IGenericRepository<Firm> FirmRepository { get; }

        private IFirmService FirmService { get; set; }

        public FirmController(IGenericRepository<Firm> firmRepository, IFirmService firmService)
        {
            FirmService = firmService;
            FirmRepository = firmRepository;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(FirmService.GetAll());
        }

        // GET: Firm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Firm/Create
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Firm firm)
        {
            if (ModelState.IsValid)
            {
                FirmRepository.Add(firm);
                return RedirectToAction("Index");
            }

            return View(firm);
        }

        // GET: Firm/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Firm firm = FirmRepository.GetSingle(id.Value);
            if (firm == null)
                return HttpNotFound();
            
            return View(firm);
        }

        // POST: Firm/Edit/5
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Firm firm)
        {
            if (ModelState.IsValid)
            {
                FirmRepository.Update(firm);
                return RedirectToAction("Index");
            }
            return View(firm);
        }

        // GET: Firm/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Firm firm = FirmRepository.GetSingle(id.Value);
            if (firm == null)
                return HttpNotFound();
            
            return View(firm);
        }

        // POST: Firm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FirmRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
