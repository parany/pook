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
            var progression = ProgressionRepository.GetSingle(id);
            return SProgression.DtoS(progression);
        }

        public void Add(SProgression entity)
        {
            ProgressionRepository.Add(SProgression.StoD(entity));
        }

        public void Update(SProgression entity)
        {
            ProgressionRepository.Update(SProgression.StoD(entity));
        }

        public void Delete(Guid id)
        {
            ProgressionRepository.Delete(id);
        }

        public ProgressionSearch SortByDate()
        {
            var now = DateTime.Now;
            var model = new ProgressionSearch
            {
                EndDate = now,
                StartDate = now.AddDays(-90)
            };
            var progressions = ProgressionRepository
                .GetList(p => p.Date >= model.StartDate && p.Date <= model.EndDate)
                .Select(SProgression.DtoS)
                .ToList();
            model.Progressions = progressions;

            var books = BookRepository.GetAll();
            books.Insert(0, null);
            model.Books = new SelectList(books, "Id", "Title");
            var statuses = StatusRepository.GetAll();
            statuses.Insert(0, null);
            model.Statuses = new SelectList(statuses, "Id", "Title");

            return model;
        }

        public List<SProgression> Search(ProgressionSearch search)
        {
            var progressions = ProgressionRepository
                .GetList(p =>
                    (search.BookId == null || p.BookId == search.BookId)
                    && (search.StatusId == null || p.StatusId == search.StatusId)
                    && p.Date >= search.StartDate && p.Date <= search.EndDate)
                .Select(SProgression.DtoS)
                .ToList();
            return progressions;
        }

        public List<ProgressionSection> SortByBook(string userId)
        {
            var progressions = ProgressionRepository.GetList(p => p.UserId == userId);
            var books = progressions.Select(p => p.Book);
            var progressionSections =
                (from p in progressions
                 orderby p.Book.Title
                 group p by p.Book.Id into g
                 select new ProgressionSection
                 {
                     Book = books.First(b => b.Id == g.Key).Title,
                     BookId = g.Key,
                     Progressions = g.Select(SProgression.DtoS).ToList()
                 }).ToList();
            return progressionSections;
        }

        public List<SProgression> GetByBook(string userId, Guid bookId)
        {
            var progressions = ProgressionRepository
                .GetList(p => p.UserId == userId && p.BookId == bookId)
                .Select(SProgression.DtoS)
                .ToList();
            return progressions;
        }

        public ProgressionCreate BuildProgressionCreate(Guid bookId)
        {
            var progressionCreate = new ProgressionCreate
            {
                Progression = new SProgression
                {
                    BookId = bookId,
                    Date = DateTime.Now,
                },
                StatusList = new SelectList(StatusRepository.GetAll(), "Id", "Title")
            };
            return progressionCreate;
        }

        public ProgressionCreate BuildProgressionEdit(Guid bookId)
        {
            var progression = GetSingle(bookId);
            var progressionCreate = new ProgressionCreate
            {
                Progression = progression,
                StatusList = new SelectList(StatusRepository.GetAll(), "Id", "Title", progression.StatusId),
                BookList = new SelectList(BookRepository.GetAll(), "Id", "Title", progression.BookId)
            };
            return progressionCreate;
        }
    }
}
