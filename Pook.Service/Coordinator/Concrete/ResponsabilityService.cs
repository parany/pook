using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using Pook.Service.Models.ResponsabilityTypes;
using SResponsability = Pook.Service.Models.ResponsabilityTypes.Responsability;
using DResponsability = Pook.Data.Entities.Responsability;
using ResponsabilityType = Pook.Data.Entities.ResponsabilityType;
using SAuthor = Pook.Service.Models.Authors.Author;
using DAuthor = Pook.Data.Entities.Author;

namespace Pook.Service.Coordinator.Concrete
{
    public class ResponsabilityService : IResponsabilityService
    {
        private IGenericRepository<DResponsability> ResponsabilityRepository { get; }

        private IGenericRepository<DAuthor> AuthorRepository { get; }

        private IGenericRepository<ResponsabilityType> ResponsabilityTypeRepository { get; }

        public ResponsabilityService(
            IGenericRepository<DResponsability> responsabilityRepository,
            IGenericRepository<ResponsabilityType> responsabilityTypeRepository,
            IGenericRepository<Author> authorRepository
            )
        {

            ResponsabilityRepository = responsabilityRepository;
            ResponsabilityTypeRepository = responsabilityTypeRepository;
            AuthorRepository = authorRepository;

            ResponsabilityRepository.AddNavigationProperties(
                r => r.Author,
                r => r.Book,
                r => r.ResponsabilityType
                );
        }

        public IList<SResponsability> GetAll()
        {
            return ResponsabilityRepository
                .GetAll()
                .Select(SResponsability.DtoS)
                .ToList();
        }

        public SResponsability GetSingle(Guid id)
        {
            var entity = ResponsabilityRepository.GetSingle(id);
            return SResponsability.DtoS(entity);
        }

        public void Add(SResponsability entity)
        {
            ResponsabilityRepository.Add(SResponsability.StoD(entity));
        }

        public void Update(SResponsability entity)
        {
            ResponsabilityRepository.Update(SResponsability.StoD(entity));
        }

        public void Delete(Guid id)
        {
            ResponsabilityRepository.Delete(id);
        }

        public ResponsabilityCreate BuildResponsabilityCreate()
        {
            var authors = AuthorRepository.GetAll().Select(SAuthor.DtoS).ToList();
            var responsabilityTypes = ResponsabilityTypeRepository.GetAll();
            var responsabilityCreate = new ResponsabilityCreate
            {
                AuthorList = new SelectList(authors, "Id", "FullName"),
                ResponsabilityTypeList = new SelectList(responsabilityTypes, "Id", "Title"),
                Responsability = new SResponsability()
            };
            return responsabilityCreate;
        }
    }
}
