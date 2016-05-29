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
    public class BookController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Editor).Include(b => b.Firm);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.EditorId = new SelectList(db.Editors, "EditorId", "Title");
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Title");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,Description,ReleaseDate,FirmId,EditorId,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.BookId = Guid.NewGuid();
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditorId = new SelectList(db.Editors, "EditorId", "Title", book.EditorId);
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Title", book.FirmId);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditorId = new SelectList(db.Editors, "EditorId", "Title", book.EditorId);
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Title", book.FirmId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,Description,ReleaseDate,FirmId,EditorId,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditorId = new SelectList(db.Editors, "EditorId", "Title", book.EditorId);
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Title", book.FirmId);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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
