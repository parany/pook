using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using SResponsabilityType = Pook.Service.Models.ResponsabilityTypes.ResponsabilityType;
using DResponsabilityType = Pook.Data.Entities.ResponsabilityType;

namespace Pook.Service.Coordinator.Concrete
{
    public class ResponsabilityTypeService : IResponsabilityTypeService
    {
        private IGenericRepository<DResponsabilityType> ResponsabilityTypeRepository { get; set; }

        public ResponsabilityTypeService(IGenericRepository<DResponsabilityType> responsabilityTypeRepository)
        {
            ResponsabilityTypeRepository = responsabilityTypeRepository;
        }

        public IList<SResponsabilityType> GetAll()
        {
            return ResponsabilityTypeRepository
                .GetAll()
                .Select(SResponsabilityType.DtoS)
                .ToList();
        }

        public SResponsabilityType GetSingle(Guid id)
        {
            var entity = ResponsabilityTypeRepository.GetSingle(id);
            return SResponsabilityType.DtoS(entity);
        }

        public void Add(SResponsabilityType entity)
        {
            ResponsabilityTypeRepository.Add(SResponsabilityType.StoD(entity));
        }

        public void Update(SResponsabilityType entity)
        {
            ResponsabilityTypeRepository.Update(SResponsabilityType.StoD(entity));
        }

        public void Delete(Guid id)
        {
            ResponsabilityTypeRepository.Delete(id);
        }
    }
}
