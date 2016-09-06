using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.Notes;
using Pook.Service.Models.Progressions;
using Pook.Service.Models.Users;
using SUser = Pook.Service.Models.Users.User;
using DUser = Pook.Data.Entities.User;
using DNote = Pook.Data.Entities.Note;
using SNote = Pook.Service.Models.Notes.Note;
using DProgression = Pook.Data.Entities.Progression;
using SProgression = Pook.Service.Models.Progressions.Progression;

namespace Pook.Service.Coordinator.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository { get; }

        private IGenericRepository<DProgression> ProgressionRepository { get; }

        private IGenericRepository<DNote> NoteRepository { get; }


        public UserService(
            IUserRepository userRepository,
            IGenericRepository<DProgression> progressionRepository,
            IGenericRepository<DNote> noteRepository
            )
        {
            UserRepository = userRepository;
            ProgressionRepository = progressionRepository;
            NoteRepository = noteRepository;

            ProgressionRepository.SetSortExpression(list => list.OrderBy(p => p.Date));
            ProgressionRepository.AddNavigationProperties(
                p => p.Book,
                p => p.Status
                );
            NoteRepository.SetSortExpression(list => list.OrderBy(n => n.Page));
            NoteRepository.AddNavigationProperty(n => n.Book);
        }

        public IList<SUser> GetAll()
        {
            return UserRepository
                .GetAll()
                .Select(SUser.DtoS)
                .ToList();
        }

        public User GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDetails GetDetails(string userId)
        {
            DUser user = UserRepository.GetSingle(userId);
            var userDetails = new UserDetails { User = user };
            var progressions = ProgressionRepository.GetList(p => p.UserId == user.Id);
            var books = progressions.Select(p => p.Book).ToList();
            userDetails.ProgressionSections =
                (from p in progressions
                orderby p.Book.Title
                group p by p.Book.Id into g
                select new ProgressionSection
                {
                    Book = books.First(b => b.Id == g.Key).Title,
                    BookId = g.Key,
                    Progressions = g.Select(SProgression.DtoS).ToList()
                }).ToList();

            var notes = NoteRepository.GetList(p => p.UserId == user.Id);
            userDetails.NoteSections =
                (from p in notes
                group p by p.Book.Title into g
                select new NoteByBook
                {
                    Book = g.Key,
                    Notes = g.Select(SNote.DtoS).ToList()
                }).ToList();
            return userDetails;
        }

        public SUser GetSingle(string userId)
        {
            var user = UserRepository.GetSingle(userId);
            return SUser.DtoS(user);
        }

        public void Add(SUser user)
        {
            UserRepository.Add(SUser.StoD(user));
        }

        public void Update(SUser entity)
        {
            UserRepository.Update(SUser.StoD(entity));
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            UserRepository.Delete(id);
        }
    }
}
