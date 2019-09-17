using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Services
{
    public class PostService : IEntityService<Post>
    {
        private InstagramContext Context;
        public PostService(InstagramContext context)
        {
            Context = context;
        }

        public int Create(Post post)
        {
             Context.Posts.Add(post);
            Context.SaveChanges();

            return post.PostId;
        }

        public Post Get(Func<Post, bool> filter)
        {
            return Context.Posts
                .Include(post => post.User)
                .OrderByDescending(post => post.CreatedAt)
                .Include(_ => _.Comments)
                    .ThenInclude(thisComment => thisComment.User)
                .Where(filter).FirstOrDefault();
        }

        public int Count(Func<Post, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetMany(Func<Post, bool> filter)
        {
            return Context.Posts
                .Include(post => post.User)
                .OrderByDescending(post => post.CreatedAt)
                .Include(_ => _.Comments)
                    .ThenInclude(thisComment => thisComment.User)
                .Where(filter);
        }

        public Post Update(Action updateFn, Post post)
        {
            updateFn?.Invoke();
            Context.SaveChanges();

            return post;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

