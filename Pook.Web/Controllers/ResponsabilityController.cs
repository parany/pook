using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

namespace Pook.Web.Controllers
{
    public class ResponsabilityController : Controller
    {
        private IGenericRepository<Responsability> ResponsabilityRepository { get; }

        private IGenericRepository<Author> AuthorRepository { get; }

        private IGenericRepository<ResponsabilityType> ResponsabilityTypeRepository { get; }

        public ResponsabilityController(
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<ResponsabilityType> responsabilityTypeRepository,
            IGenericRepository<Author> authorRepository
            )
        {
            ResponsabilityRepository = responsabilityRepository;
            ResponsabilityTypeRepository = responsabilityTypeRepository;
            AuthorRepository = authorRepository;

            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author,
                r => r.Book,
                r => r.ResponsabilityType
                );
        }

        // GET: Responsability/Create
        [Route("Responsability/Create/{bookId?}")]
        public ActionResult Create(Guid? bookId)
        {
            ViewBag.AuthorId = new SelectList(AuthorRepository.GetAll(), "Id", "FullName");
            ViewBag.ResponsabilityTypeId = new SelectList(ResponsabilityTypeRepository.GetAll(), "Id", "Title");
            return View();
        }

        [Route("Responsability/Create/{bookId?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid? bookId, Responsability responsability)
        {
            if (ModelState.IsValid)
            {
                responsability.BookId = bookId.GetValueOrDefault();
                ResponsabilityRepository.Add(responsability);
                return RedirectToAction("Details", "Book", new { id = responsability.BookId });
            }

            ViewBag.AuthorId = new SelectList(AuthorRepository.GetAll(), "Id", "FullName");
            ViewBag.ResponsabilityTypeId = new SelectList(ResponsabilityTypeRepository.GetAll(), "Id", "Title");
            return View(responsability);
        }

        // GET: Responsability/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Responsability responsability = ResponsabilityRepository.GetSingle(id.Value);
            if (responsability == null)
                return HttpNotFound();

            return View(responsability);
        }

        // POST: Responsability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Responsability responsability = ResponsabilityRepository.GetSingle(id);
            ResponsabilityRepository.Delete(id);
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }
    }
}
