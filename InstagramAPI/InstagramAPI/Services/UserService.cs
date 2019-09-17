using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class UserService : IEntityService<User>
    {
        private InstagramContext Context;
        public UserService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();

            return user.UserId;
        }

        public User Get(Func<User, bool> filter)
        {
            return Context.Users.Where(filter).FirstOrDefault();
        }

        public int Count(Func<User, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetMany(Func<User, bool> filter)
        {
            throw new NotImplementedException();
        }

        public User Update(Action updateFn, User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
