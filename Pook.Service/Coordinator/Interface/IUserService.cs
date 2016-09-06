using System.Collections.Generic;
using Pook.Service.Models.Users;

namespace Pook.Service.Coordinator.Interface
{
    public interface IUserService
    {
        List<User> GetAll();
    }
}
