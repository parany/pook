using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pook.Data.Entities;
using Pook.Data.Repositories.Interface;

namespace Pook.Data.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        public IList<User> GetAll()
        {
            List<User> list;
            using (var context = new PookDbContext())
            {
                list = context.Users.ToList();
            }
            return list;
        }

        public User GetSingle(string id)
        {
            using (var context = new PookDbContext())
            {
                return context.Users.Find(id);
            }
        }

        public void Add(User user)
        {
            using (var context = new PookDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Update(User user)
        {
            using (var context = new PookDbContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var context = new PookDbContext())
            {
                var user = new User {Id = id};
                context.Users.Attach(user);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
