using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Editors;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Editor")]
    public class EditorController : Controller
    {
        private IEditorService EditorService { get; }

        public EditorController(IEditorService editorService)
        {
            EditorService = editorService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(EditorService.GetAll());
        }

        [Route("Details/{id}")]
        [NotFound]
        public ActionResult Details(Guid id)
        {
            var editor = EditorService.GetSingle(id);
            return View(editor);
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Editor editor)
        {
            EditorService.Add(editor);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            var editor = EditorService.GetSingle(id);
            return View(editor);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(Editor editor)
        {
            EditorService.Update(editor);
            return RedirectToAction("Index");
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var editor = EditorService.GetSingle(id);
            return View(editor);
        }

        [Route("Delete/{id}"), ActionName("Delete"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EditorService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
