using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using DEditor = Pook.Data.Entities.Editor;
using SEditor = Pook.Service.Models.Editors.Editor;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Editor")]
    public class EditorController : Controller
    {
        private IGenericRepository<Editor> EditorRepository { get; }

        private IEditorService EditorService { get; set; }

        public EditorController(IGenericRepository<Editor> editorRepository, IEditorService editorService)
        {
            EditorService = editorService;
            EditorRepository = editorRepository;
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
        public ActionResult Create(SEditor editor)
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
        public ActionResult Edit(SEditor editor)
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
