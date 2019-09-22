using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class CommentLikeService : IEntityService<CommentLike>
    {
        private InstagramContext Context;
        public CommentLikeService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(CommentLike commentLike)
        {
            Context.CommentLikes.Add(commentLike);
            Context.SaveChanges();

            return commentLike.CommentLikeId;
        }

        public CommentLike Get(Func<CommentLike, bool> filter)
        {
            throw new NotImplementedException();

        }

        public int Count(Func<CommentLike, bool> filter)
        {
           
            return Context.CommentLikes.Count(filter);
            
        }

        public IEnumerable<CommentLike> GetMany(Func<CommentLike, bool> filter, int skip = 0, int take = 25)
        {
            throw new NotImplementedException();

        }

        public CommentLike Update(Action updateFn, CommentLike commentLike)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return commentLike;
        }

        public void Delete(Func<CommentLike, bool> filter)
        {
            var commentLike = Context.CommentLikes.FirstOrDefault(filter);
            if (commentLike != null)
            {
                Context.CommentLikes.Remove(commentLike);
                Context.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
