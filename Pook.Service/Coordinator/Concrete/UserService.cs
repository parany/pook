using System.Collections.Generic;
using System.Linq;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using SUser = Pook.Service.Models.Users.User;
using DUser = Pook.Data.Entities.User;

namespace Pook.Service.Coordinator.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository { get; }

        private IGenericRepository<Progression> ProgressionRepository { get; }

        private IGenericRepository<Note> NoteRepository { get; }


        public UserService(
            IUserRepository userRepository,
            IGenericRepository<Progression> progressionRepository,
            IGenericRepository<Note> noteRepository
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

        public List<SUser> GetAll()
        {
            return UserRepository
                .GetAll()
                .Select(SUser.DtoS)
                .ToList();
        }
    }
}
