using Pook.Service.Models.Users;

namespace Pook.Service.Coordinator.Interface
{
    public interface IUserService : IGenericService<User>
    {
        UserDetails GetDetails(string userId);

        User GetSingle(string userId);

        void Delete(string id);
    }
}
