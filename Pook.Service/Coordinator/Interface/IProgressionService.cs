using Pook.Service.Models.Progressions;

namespace Pook.Service.Coordinator.Interface
{
    public interface IProgressionService : IGenericService<Progression>
    {
        ProgressionSearch SortByDate();
    }
}
