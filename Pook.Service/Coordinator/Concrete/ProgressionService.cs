using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Progressions;
using SProgression = Pook.Service.Models.Progressions.Progression;
using DProgression = Pook.Data.Entities.Progression;

namespace Pook.Service.Coordinator.Concrete
{
    public class ProgressionService : IProgressionService
    {
        private IGenericRepository<DProgression> ProgressionRepository { get; set; }

        private IGenericRepository<Book> BookRepository { get; set; }

        private IGenericRepository<Status> StatusRepository { get; set; }

        public ProgressionService(
            IGenericRepository<DProgression> progressionRepository,
            IGenericRepository<Book> bookRepository,
            IGenericRepository<Status> statusRepository
            )
        {
            ProgressionRepository = progressionRepository;
            BookRepository = bookRepository;
            StatusRepository = statusRepository;

            ProgressionRepository.AddNavigationProperties(
                p => p.Book,
                p => p.Status,
                p => p.User
                );
            ProgressionRepository.SetSortExpression(l => l.OrderByDescending(p => p.Date));
        }

        public IList<SProgression> GetAll()
        {
            throw new NotImplementedException();
        }

        public SProgression GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(SProgression entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SProgression entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProgressionSearch SortByDate()
        {
            var model = new ProgressionSearch();
            var now = DateTime.Now;
            model.EndDate = now;
            model.StartDate = now.AddDays(-90);
            ProgressionRepository.SetSortExpression(p => p.OrderByDescending(r => r.Date));
            var progressions = ProgressionRepository
                .GetList(p => p.Date >= model.StartDate && p.Date <= model.EndDate)
                .Select(SProgression.DtoS)
                .ToList();

            var books = BookRepository.GetAll();
            books.Insert(0, null);
            model.Books = new SelectList(books, "Id", "Title");
            var statuses = StatusRepository.GetAll();
            statuses.Insert(0, null);
            model.Statuses = new SelectList(statuses, "Id", "Title");

            model.Progressions = progressions;

            return model;
        }
    }
}
