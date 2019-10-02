using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using Microsoft.EntityFrameworkCore;
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
            return Context.Users.Count(filter);
        }

        public IEnumerable<User> GetMany(Func<User, bool> filter, int skip = 0, int take = 25)
        {
            //var users = Context.Users
            //    //.Include(post => post.User)
            //    .OrderByDescending(user => user.Created)
            //    //.Include(_ => _.Comments)
            //    //    .ThenInclude(thisComment => thisComment.User)
            //    //.Include(_ => _.UserFriends)
            //    .Where(filter)
            //    .Skip(skip)
            //    .Take(take)
            //    .ToList()
            //    ;
            //return users;

            return Context.Users.Where(filter).Skip(skip).Take(take);

        }

        public User Update(Action updateFn, User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Func<User, bool> filter)
        {
            throw new NotImplementedException();


        }
    }
}
