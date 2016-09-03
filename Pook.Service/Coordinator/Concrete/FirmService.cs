using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DFirm = Pook.Data.Entities.Firm;
using SFirm = Pook.Service.Models.Firms.Firm;

namespace Pook.Service.Coordinator.Concrete
{
    public class FirmService : IFirmService
    {
        private IGenericRepository<DFirm> FirmRepository { get; }

        public FirmService(IGenericRepository<DFirm> firmRepository)
        {
            FirmRepository = firmRepository;
        }

        public IList<SFirm> GetAll()
        {
            return FirmRepository
                .GetAll()
                .Select(DtoS)
                .ToList();
        }

        public SFirm GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(SFirm author)
        {
            throw new NotImplementedException();
        }

        public void Update(SFirm author)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        private SFirm DtoS(DFirm firm)
        {
            return new SFirm
            {
                Id = firm.Id,
                Title = firm.Title,
                Description = firm.Description,
                Address = firm.Address
            };
        }

        private DFirm StoD(SFirm firm)
        {
            return new DFirm
            {
                Id = firm.Id,
                Title = firm.Title,
                Description = firm.Description,
                Address = firm.Address
            };
        }
    }
}
