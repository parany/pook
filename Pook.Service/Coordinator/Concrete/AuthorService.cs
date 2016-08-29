using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DAuthor = Pook.Data.Entities.Author;
using SAuthor = Pook.Service.Models.Author;

namespace Pook.Service.Coordinator.Concrete
{
    public class AuthorService : IAuthorService
    {
        private IGenericRepository<DAuthor> AuthorRepository { get; }

        public AuthorService(IGenericRepository<DAuthor> authorRepository)
        {
            AuthorRepository = authorRepository;
            AuthorRepository.SetSortExpression(
                list => list.OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
                );
        }

        public IList<SAuthor> GetAll()
        {
            var authors = AuthorRepository.GetAll();
            return authors.Select(a => new SAuthor
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName
            }).ToList();
        }

        public SAuthor GetSingle(Guid id)
        {
            var author = AuthorRepository.GetSingle(id);
            return new SAuthor
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description,
                Address = author.Address,
                Email = author.Email
            };
        }

        public void Add(SAuthor author)
        {
            AuthorRepository.Add(new DAuthor
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Address = author.Address,
                Description = author.Description,
            });
        }

        public void Update(SAuthor author)
        {
            AuthorRepository.Update(new DAuthor
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Address = author.Address,
                Description = author.Description,
            });
        }
    }
}
