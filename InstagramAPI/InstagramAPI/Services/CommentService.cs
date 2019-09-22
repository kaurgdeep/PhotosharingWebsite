using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class CommentService : IEntityService<Comment>
    {
        private InstagramContext Context;
        public CommentService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(Comment comment)
        {
            Context.Comments.Add(comment);
            Context.SaveChanges();

            return comment.CommentId;
        }

        public Comment Get(Func<Comment, bool> filter)
        {
            return Context.Comments
                    .Include(_ => _.User)
                    .OrderByDescending(_ => _.CreatedAt)
                    .Include(_ => _.Post)
                        .ThenInclude(_ => _.User)
                    .Where(filter).FirstOrDefault();
        }

        public int Count(Func<Comment, bool> filter)
        {
            return Context.Comments.Count(filter);

        }

        public IEnumerable<Comment> GetMany(Func<Comment, bool> filter, int skip = 0, int take = 25)
        {
            take = take <= 0 ? 25 : take;
            return Context.Comments
                   // .Include(_ => _.User)
                    .OrderByDescending(_ => _.CreatedAt)
                    // .Include(_ => _.Post)
                    // .ThenInclude(_ => _.User)
                    .Skip(skip)
                    .Take(take)
                    .Where(filter);
        }

        public Comment Update(Action updateFn, Comment comment)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return comment;
        }

        public void Delete(Func<Comment, bool> filter)
        {
           
            throw new NotImplementedException();
           
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
