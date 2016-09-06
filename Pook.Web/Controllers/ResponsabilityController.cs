using System;
using System.Net;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Web.Filters;
using SResponsability = Pook.Service.Models.ResponsabilityTypes.Responsability;
using DResponsability = Pook.Data.Entities.Responsability;

namespace Pook.Web.Controllers
{
    [RoutePrefix("Responsability")]
    public class ResponsabilityController : Controller
    {
        private IGenericRepository<DResponsability> ResponsabilityRepository { get; }

        private IGenericRepository<Author> AuthorRepository { get; }

        private IGenericRepository<ResponsabilityType> ResponsabilityTypeRepository { get; }

        private IResponsabilityService ResponsabilityService { get; set; }

        public ResponsabilityController(
            IGenericRepository<Responsability> responsabilityRepository,
            IGenericRepository<ResponsabilityType> responsabilityTypeRepository,
            IGenericRepository<Author> authorRepository,
            IResponsabilityService responsabilityService
            )
        {
            ResponsabilityService = responsabilityService;

            ResponsabilityRepository = responsabilityRepository;
            ResponsabilityTypeRepository = responsabilityTypeRepository;
            AuthorRepository = authorRepository;

            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author,
                r => r.Book,
                r => r.ResponsabilityType
                );
        }

        [Route("Create/{bookId?}"), HttpGet]
        public ActionResult Create(Guid? bookId)
        {
            var responsabilityCreate = ResponsabilityService.BuildResponsabilityCreate();
            return View(responsabilityCreate);
        }

        [Route("Create/{bookId}"), HttpPost]
        [ValidateAntiForgeryToken, ValidateModel]
        public ActionResult Create(Guid bookId, SResponsability responsability)
        {
            responsability.BookId = bookId;
            ResponsabilityService.Add(responsability);
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }

        [Route("Delete/{id}")]
        [NotFound]
        public ActionResult Delete(Guid id)
        {
            var responsability = ResponsabilityService.GetSingle(id);
            return View(responsability);
        }

        [Route("Delete/{id}"), HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var responsability = ResponsabilityService.GetSingle(id);
            ResponsabilityRepository.Delete(id);
            return RedirectToAction("Details", "Book", new { id = responsability.BookId });
        }
    }
}
