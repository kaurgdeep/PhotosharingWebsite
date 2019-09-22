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
            return Context.Posts.Count(filter);
        }

        public IEnumerable<Post> GetMany(Func<Post, bool> filter, int skip = 0, int take = 25)
        {
            return Context.Posts
                //.Include(post => post.User)
                .OrderByDescending(post => post.CreatedAt)
                //.Include(_ => _.Comments)
                //    .ThenInclude(thisComment => thisComment.User)
                .Include(_ => _.PostLikes)
                .Where(filter)
                .Skip(skip)
                .Take(take)
                //.ToList()
                ;
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

        public void Delete(Func<Post, bool> filter)
        {
            throw new NotImplementedException();


        }
    }
}

