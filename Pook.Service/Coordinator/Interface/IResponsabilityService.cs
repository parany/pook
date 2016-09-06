using Pook.Service.Models.ResponsabilityTypes;

namespace Pook.Service.Coordinator.Interface
{
    public interface IResponsabilityService : IGenericService<Responsability>
    {
        ResponsabilityCreate BuildResponsabilityCreate();
    }
}
