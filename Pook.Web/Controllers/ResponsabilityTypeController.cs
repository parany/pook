using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

namespace Pook.Web.Controllers
{
    public class ResponsabilityTypeController : Controller
    {
        private IGenericRepository<ResponsabilityType> ResponsabilityTypeRepository { get; set; }

        public ResponsabilityTypeController(IGenericRepository<ResponsabilityType> responsabilityTypeRepository)
        {
            ResponsabilityTypeRepository = responsabilityTypeRepository;
        }

        // GET: ResponsabilityType
        public ActionResult Index()
        {
            return View(ResponsabilityTypeRepository.GetAll());
        }

        // GET: ResponsabilityType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResponsabilityType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResponsabilityType responsabilityType)
        {
            if (ModelState.IsValid)
            {
                ResponsabilityTypeRepository.Add(responsabilityType);
                return RedirectToAction("Index");
            }

            return View(responsabilityType);
        }

        // GET: ResponsabilityType/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            ResponsabilityType responsabilityType = ResponsabilityTypeRepository.GetSingle(id.Value);
            if (responsabilityType == null)
                return HttpNotFound();
            
            return View(responsabilityType);
        }

        // POST: ResponsabilityType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResponsabilityType responsabilityType)
        {
            if (ModelState.IsValid)
            {
                ResponsabilityTypeRepository.Update(responsabilityType);
                return RedirectToAction("Index");
            }
            return View(responsabilityType);
        }

        // GET: ResponsabilityType/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            ResponsabilityType responsabilityType = ResponsabilityTypeRepository.GetSingle(id.Value);
            if (responsabilityType == null)
                return HttpNotFound();
            
            return View(responsabilityType);
        }

        // POST: ResponsabilityType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResponsabilityTypeRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
