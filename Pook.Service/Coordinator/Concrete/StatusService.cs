using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DStatus = Pook.Data.Entities.Status;
using SStatus = Pook.Service.Models.Statuses.Status;

namespace Pook.Service.Coordinator.Concrete
{
    public class StatusService : IStatusService
    {
        private IGenericRepository<DStatus> StatusRepository { get; }

        public StatusService(IGenericRepository<DStatus> statusRepository)
        {
            StatusRepository = statusRepository;
        }


        public IList<SStatus> GetAll()
        {
            return StatusRepository
                .GetAll()
                .Select(SStatus.DtoS)
                .ToList();
        }

        public SStatus GetSingle(Guid id)
        {
            var status = StatusRepository.GetSingle(id);
            return SStatus.DtoS(status);
        }

        public void Add(SStatus entity)
        {
            StatusRepository.Add(SStatus.StoD(entity));
        }

        public void Update(SStatus entity)
        {
            StatusRepository.Update(SStatus.StoD(entity));
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
