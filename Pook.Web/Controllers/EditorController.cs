using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

namespace Pook.Web.Controllers
{
    public class EditorController : Controller
    {
        private IGenericRepository<Editor> EditorRepository { get; }

        public EditorController(IGenericRepository<Editor> editorRepository)
        {
            EditorRepository = editorRepository;
        }

        // GET: Editor
        public ActionResult Index()
        {
            return View(EditorRepository.GetAll());
        }

        // GET: Editor/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Editor editor = EditorRepository.GetSingle(id.Value);
            if (editor == null)
                return HttpNotFound();
            
            return View(editor);
        }

        // GET: Editor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editor/Create
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Editor editor)
        {
            if (ModelState.IsValid)
            {
                EditorRepository.Add(editor);
                return RedirectToAction("Index");
            }

            return View(editor);
        }

        // GET: Editor/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Editor editor = EditorRepository.GetSingle(id.Value);
            if (editor == null)
                return HttpNotFound();
            
            return View(editor);
        }

        // POST: Editor/Edit/5
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Editor editor)
        {
            if (ModelState.IsValid)
            {
                EditorRepository.Update(editor);
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        // GET: Editor/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Editor editor = EditorRepository.GetSingle(id.Value);
            if (editor == null)
                return HttpNotFound();
            
            return View(editor);
        }

        // POST: Editor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EditorRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
