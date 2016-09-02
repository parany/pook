using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Category")]
    public class CategoryController : Controller
    {
        private IGenericRepository<Category> CategoryRepository { get; }

        private ICategoryService CategoryService { get; set; }

        public CategoryController(
            ICategoryService categoryService,
            IGenericRepository<Category> categoryRepository
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

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Category category = CategoryRepository.GetSingle(id.Value);

            if (category == null)
                return HttpNotFound();
            
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Category category = CategoryRepository.GetSingle(id.Value);

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CategoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
