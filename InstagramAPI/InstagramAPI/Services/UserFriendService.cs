using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class UserFriendService : IEntityService<UserFriend>
    {
        private InstagramContext Context;
        public UserFriendService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(UserFriend userFriend)
        {
            Context.UserFriends.Add(userFriend);
            Context.SaveChanges();

            return userFriend.FriendId;
        }

        public UserFriend Get(Func<UserFriend, bool> filter)
        {
            throw new NotImplementedException();

        }

        public int Count(Func<UserFriend, bool> filter)
        {
            return Context.UserFriends.Count(filter);
        }

        public IEnumerable<UserFriend> GetMany(Func<UserFriend, bool> filter, int skip = 0, int take = 25)
        {
            throw new NotImplementedException();

        }

        public UserFriend Update(Action updateFn, UserFriend userFriend)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return userFriend;
        }

        public void Delete(Func<UserFriend, bool> filter)
        {
            var userFriend = Context.UserFriends.FirstOrDefault(filter);
            if (userFriend != null)
            {
                Context.UserFriends.Remove(userFriend);
                Context.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
