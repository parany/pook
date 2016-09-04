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
            var firm = FirmRepository.GetSingle(id);
            return DtoS(firm);
        }

        public void Add(SFirm firm)
        {
            FirmRepository.Add(StoD(firm));
        }

        public void Update(SFirm firm)
        {
            FirmRepository.Update(StoD(firm));
        }

        public void Delete(Guid id)
        {
            FirmRepository.Delete(id);
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
