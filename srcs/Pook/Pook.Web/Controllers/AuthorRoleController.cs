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
    public class AuthorRoleController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: AuthorRole
        public ActionResult Index()
        {
            return View(db.AuthorRoles.ToList());
        }

        // GET: AuthorRole/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorRole authorRole = db.AuthorRoles.Find(id);
            if (authorRole == null)
            {
                return HttpNotFound();
            }
            return View(authorRole);
        }

        // GET: AuthorRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorRoleId,Title,Desription,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] AuthorRole authorRole)
        {
            if (ModelState.IsValid)
            {
                authorRole.AuthorRoleId = Guid.NewGuid();
                db.AuthorRoles.Add(authorRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authorRole);
        }

        // GET: AuthorRole/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorRole authorRole = db.AuthorRoles.Find(id);
            if (authorRole == null)
            {
                return HttpNotFound();
            }
            return View(authorRole);
        }

        // POST: AuthorRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorRoleId,Title,Desription,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] AuthorRole authorRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authorRole);
        }

        // GET: AuthorRole/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorRole authorRole = db.AuthorRoles.Find(id);
            if (authorRole == null)
            {
                return HttpNotFound();
            }
            return View(authorRole);
        }

        // POST: AuthorRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AuthorRole authorRole = db.AuthorRoles.Find(id);
            db.AuthorRoles.Remove(authorRole);
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
