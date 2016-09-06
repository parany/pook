using System.Collections.Generic;
using Pook.Data.Entities;

namespace Pook.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        IList<User> GetAll();

        User GetSingle(string id);

        void Add(User user);

        void Update(User user);

        void Delete(string id);
    }
}
