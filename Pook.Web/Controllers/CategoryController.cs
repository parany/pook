using System;
using System.Web.Mvc;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Categories;
using Pook.Web.Filters;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Category")]
    public class CategoryController : Controller
    {
        private ICategoryService CategoryService { get; }

        public CategoryController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(CategoryService.GetAll());
        }

        [Route("Create"), HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Category category)
        {
            CategoryService.Add(category);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            Category category = CategoryService.GetSingle(id);
            return View(category);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(Category category)
        {
            CategoryService.Update(category);
            return RedirectToAction("Index");
        }
    }
}
