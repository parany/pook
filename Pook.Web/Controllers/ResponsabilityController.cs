using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pook.Data;
using Pook.Data.Entities;

namespace Pook.Web.Controllers
{
    public class ResponsabilityController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: Responsability
        public ActionResult Index()
        {
            var responsabilities = db.Responsabilities.Include(r => r.Author).Include(r => r.Book).Include(r => r.ResponsabilityType);
            return View(responsabilities.ToList());
        }

        // GET: Responsability/Create
        [Route("Responsability/Create/{bookId?}")]
        public ActionResult Create(Guid? bookId)
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FullName");
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", bookId.GetValueOrDefault());
            ViewBag.ResponsabilityTypeId = new SelectList(db.ResponsabilityTypes, "ResponsabilityTypeId", "Title");
            return View();
        }

        [Route("Responsability/Create/{bookId?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Responsability responsability)
        {
            if (ModelState.IsValid)
            {
                responsability.Id = Guid.NewGuid();
                db.Responsabilities.Add(responsability);
                db.SaveChanges();
                return RedirectToAction("Details", "Book", new { id = responsability.BookId });
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", responsability.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", responsability.BookId);
            ViewBag.ResponsabilityTypeId = new SelectList(db.ResponsabilityTypes, "ResponsabilityTypeId", "Title", responsability.ResponsabilityTypeId);
            return View(responsability);
        }

        // GET: Responsability/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsability responsability = db.Responsabilities.Find(id);
            if (responsability == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FullName", responsability.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", responsability.BookId);
            ViewBag.ResponsabilityTypeId = new SelectList(db.ResponsabilityTypes, "ResponsabilityTypeId", "Title", responsability.ResponsabilityTypeId);
            return View(responsability);
        }

        // POST: Responsability/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponsabilityId,ResponsabilityTypeId,AuthorId,BookId")] Responsability responsability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Book", new { id = responsability.BookId });
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "FirstName", responsability.AuthorId);
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", responsability.BookId);
            ViewBag.ResponsabilityTypeId = new SelectList(db.ResponsabilityTypes, "ResponsabilityTypeId", "Title", responsability.ResponsabilityTypeId);
            return View(responsability);
        }

        // GET: Responsability/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsability responsability = db.Responsabilities.Find(id);
            if (responsability == null)
            {
                return HttpNotFound();
            }
            return View(responsability);
        }

        // POST: Responsability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Responsability responsability = db.Responsabilities.Find(id);
            db.Responsabilities.Remove(responsability);
            db.SaveChanges();
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
