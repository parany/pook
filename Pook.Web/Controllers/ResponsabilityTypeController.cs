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
    public class ResponsabilityTypeController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: ResponsabilityType
        public ActionResult Index()
        {
            return View(db.ResponsabilityTypes.ToList());
        }

        // GET: ResponsabilityType/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsabilityType responsabilityType = db.ResponsabilityTypes.Find(id);
            if (responsabilityType == null)
            {
                return HttpNotFound();
            }
            return View(responsabilityType);
        }

        // GET: ResponsabilityType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResponsabilityType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponsabilityTypeId,Title,Desription")] ResponsabilityType responsabilityType)
        {
            if (ModelState.IsValid)
            {
                responsabilityType.ResponsabilityTypeId = Guid.NewGuid();
                db.ResponsabilityTypes.Add(responsabilityType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(responsabilityType);
        }

        // GET: ResponsabilityType/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsabilityType responsabilityType = db.ResponsabilityTypes.Find(id);
            if (responsabilityType == null)
            {
                return HttpNotFound();
            }
            return View(responsabilityType);
        }

        // POST: ResponsabilityType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponsabilityTypeId,Title,Desription")] ResponsabilityType responsabilityType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(responsabilityType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(responsabilityType);
        }

        // GET: ResponsabilityType/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResponsabilityType responsabilityType = db.ResponsabilityTypes.Find(id);
            if (responsabilityType == null)
            {
                return HttpNotFound();
            }
            return View(responsabilityType);
        }

        // POST: ResponsabilityType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ResponsabilityType responsabilityType = db.ResponsabilityTypes.Find(id);
            db.ResponsabilityTypes.Remove(responsabilityType);
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