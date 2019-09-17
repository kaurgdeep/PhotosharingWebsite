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
                    .Include(_ => _.Post)
                    .OrderByDescending(_ => _.CreatedAt)
                    .Include(_ => _.User)
                    .Where(filter).FirstOrDefault();
        }

        public int Count(Func<Comment, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetMany(Func<Comment, bool> filter)
        {
            return Context.Comments
                .Include(_ => _.Post)
                .OrderByDescending(_ => _.CreatedAt)
                .Include(_ => _.User)
                .Where(filter);
        }

        public Comment Update(Action updateFn, Comment comment)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return comment;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
