using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using DCategory = Pook.Data.Entities.Category;
using SCategory = Pook.Service.Models.Categories.Category;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Category")]
    public class CategoryController : Controller
    {
        private IGenericRepository<DCategory> CategoryRepository { get; }

        private ICategoryService CategoryService { get; set; }

        public CategoryController(
            ICategoryService categoryService,
            IGenericRepository<DCategory> categoryRepository
            )
        {
            CategoryService = categoryService;
            CategoryRepository = categoryRepository;
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
        public ActionResult Create(SCategory category)
        {
            CategoryService.Add(category);
            return RedirectToAction("Index");
        }

        [Route("Edit/{id}")]
        [NotFound]
        public ActionResult Edit(Guid id)
        {
            SCategory category = CategoryService.GetSingle(id);
            return View(category);
        }

        [Route("Edit/{id}"), HttpPost]
        [ValidateInput(false), ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Edit(SCategory category)
        {
            CategoryService.Update(category);
            return RedirectToAction("Index");
        }
    }
}
