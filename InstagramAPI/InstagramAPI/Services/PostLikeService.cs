using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class PostLikeService : IEntityService<PostLike>
    {
        private InstagramContext Context;
        public PostLikeService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(PostLike postLike)
        {
            Context.PostLikes.Add(postLike);
            Context.SaveChanges();

            return postLike.PostLikeId;
        }

        public PostLike Get(Func<PostLike, bool> filter)
        {
            throw new NotImplementedException();

        }

        public int Count(Func<PostLike, bool> filter)
        {
            return Context.PostLikes.Count(filter);
        }

        public IEnumerable<PostLike> GetMany(Func<PostLike, bool> filter, int skip = 0, int take = 25)
        {
            throw new NotImplementedException();

        }

        public PostLike Update(Action updateFn, PostLike postLike)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return postLike;
        }

        public void Delete(Func<PostLike, bool> filter)
        {
            var postLike = Context.PostLikes.FirstOrDefault(filter);
            if (postLike != null)
            {
                Context.PostLikes.Remove(postLike);
                Context.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
