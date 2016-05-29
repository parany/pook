﻿using System;
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
    public class AuthorController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: Author
        public ActionResult Index()
        {
            var authors = db.Authors.Include(a => a.AuthorRole);
            return View(authors.ToList());
        }

        // GET: Author/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            ViewBag.AuthorRoleId = new SelectList(db.AuthorRoles, "AuthorRoleId", "Title");
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorId,AuthorRoleId,FirstName,LastName,Description,Email,Address,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] Author author)
        {
            if (ModelState.IsValid)
            {
                author.AuthorId = Guid.NewGuid();
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorRoleId = new SelectList(db.AuthorRoles, "AuthorRoleId", "Title", author.AuthorRoleId);
            return View(author);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorRoleId = new SelectList(db.AuthorRoles, "AuthorRoleId", "Title", author.AuthorRoleId);
            return View(author);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,AuthorRoleId,FirstName,LastName,Description,Email,Address,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorRoleId = new SelectList(db.AuthorRoles, "AuthorRoleId", "Title", author.AuthorRoleId);
            return View(author);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
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
