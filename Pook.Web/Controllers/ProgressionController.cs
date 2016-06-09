﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Pook.Data;
using Pook.Data.Entities;

namespace Pook.Web.Controllers
{
    public class ProgressionController : Controller
    {
        private PookDbContext db = new PookDbContext();

        // GET: Progression
        public ActionResult Index()
        {
            var progressions = db.Progressions
                .OrderByDescending(p => p.Date)
                .Include(p => p.Book)
                .Include(p => p.Status)
                .Include(p => p.User)
                .AsNoTracking()
                .ToList();
            
            var books = progressions
                .Select(p => p.Book)
                .GroupBy(p => p.BookId)
                .Select(p => p.First())
                .ToList();
            books.Insert(0, null);
            ViewBag.bookId = new SelectList(books, "BookId", "Title");
            var statuses = db.Statuses.AsNoTracking().ToList();
            statuses.Insert(0, null);
            ViewBag.statusId = new SelectList(statuses, "StatusId", "Title");
            var users = db.Users.ToList();
            users.Insert(0, null);
            ViewBag.userId = new SelectList(users, "Id", "FirstName");

            progressions = progressions.Select(p => new Progression
            {
                Date = p.Date,
                Book = p.Book,
                Status = new Status { Title = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title },
                User = p.User
            }).ToList();

            return View(progressions);
        }

        // GET: Progression/Search
        public ActionResult Search(Guid? bookId, Guid? statusId, string userId)
        {
            var progressions = db.Progressions
                .OrderByDescending(p => p.Date)
                .Include(p => p.Book)
                .Include(p => p.Status)
                .Include(p => p.User)
                .Where(p =>
                    (bookId == null || p.BookId == bookId)
                    && (statusId == null || p.StatusId == statusId)
                    && (string.IsNullOrEmpty(userId) || p.UserId == userId)
                )
                .ToList();

            progressions = progressions.Select(p => new Progression
            {
                Date = p.Date,
                Book = p.Book,
                Status = new Status { Title = p.Status.Title == "Current" ? p.Page.ToString() : p.Status.Title },
                User = p.User
            }).ToList();

            return PartialView(progressions);
        }

        // GET: Progression/PageProgress
        public ActionResult PageProgress(string userId, Guid bookId)
        {
            var progressions = db.Progressions
                .OrderByDescending(p => p.Page)
                .Include(p => p.Status)
                .Where(p => p.Status.Title == "Current" && p.UserId == userId && p.BookId == bookId)
                .ToList();
            return View(progressions);
        }

        // GET: Progression/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Progression/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgressionId,StatusId,BookId,UserId,Date,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle,Page")] Progression progression)
        {
            if (ModelState.IsValid)
            {
                progression.ProgressionId = Guid.NewGuid();
                db.Progressions.Add(progression);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", progression.BookId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Title", progression.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", progression.UserId);
            return View(progression);
        }

        // GET: Progression/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Progression progression = db.Progressions.Find(id);
            if (progression == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", progression.BookId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Title", progression.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", progression.UserId);
            return View(progression);
        }

        // POST: Progression/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgressionId,StatusId,BookId,UserId,Date,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy,SeoTitle,Page")] Progression progression)
        {
            if (ModelState.IsValid)
            {
                db.Entry(progression).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", progression.BookId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Title", progression.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", progression.UserId);
            return View(progression);
        }

        // GET: Progression/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Progression progression = db.Progressions.Find(id);
            if (progression == null)
            {
                return HttpNotFound();
            }
            return View(progression);
        }

        // POST: Progression/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Progression progression = db.Progressions.Find(id);
            db.Progressions.Remove(progression);
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
