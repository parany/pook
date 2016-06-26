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
    public class EditorController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: Editor
        public ActionResult Index()
        {
            return View(db.Editors.ToList());
        }

        // GET: Editor/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editor editor = db.Editors.Find(id);
            if (editor == null)
            {
                return HttpNotFound();
            }
            return View(editor);
        }

        // GET: Editor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Editor editor)
        {
            if (ModelState.IsValid)
            {
                editor.Id = Guid.NewGuid();
                db.Editors.Add(editor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editor);
        }

        // GET: Editor/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editor editor = db.Editors.Find(id);
            if (editor == null)
            {
                return HttpNotFound();
            }
            return View(editor);
        }

        // POST: Editor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditorId,Title,Description,Address,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] Editor editor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        // GET: Editor/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editor editor = db.Editors.Find(id);
            if (editor == null)
            {
                return HttpNotFound();
            }
            return View(editor);
        }

        // POST: Editor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Editor editor = db.Editors.Find(id);
            db.Editors.Remove(editor);
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
