using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramAPI.Dtos;
using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using InstagramAPI.Responses;
using InstagramAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstagramAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors(Constants.InstagramServerCorsPolicy)]
    // [ApiController]
    public class PostsController : InstagramBaseController
    {
        private IEntityService<User> UserService;
        private IEntityService<Post> PostService;
        private IEntityService<Comment> CommentService;
        private IEntityService<CommentLike> CommentLikeService;
        private IEntityService<PostLike> PostLikeService;



        public PostsController(IEntityService<User> userService, IEntityService<Post> postService, IEntityService<PostLike> postLikeService, IEntityService<Comment> commentService, IEntityService<CommentLike> commentLikeService)
        {
            UserService = userService;
            PostService = postService;
            CommentService = commentService;
            PostLikeService = postLikeService;
            CommentLikeService = commentLikeService;

        }
        [HttpPost]
        [Route("")]
        public ActionResult CreatePost([FromBody] PostDto postDto)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }

            var post = new Post
            {
                UserId = LoggedInUserId.Value,
                PostId = postDto.PostId,
                PostText = postDto.PostText,
                ImagePath = postDto.ImagePath,
                CreatedAt = DateTime.UtcNow,


            };
            var postId = PostService.Create(post);

            return Ok(new CreateResponse { Id = postId });

        }

        [HttpGet]
        [Route("")]
        public ActionResult GetPost([FromQuery]int skip, [FromQuery]int take)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }


            var posts = PostService.GetMany(x => x.UserId == LoggedInUserId, skip, take);

            return Ok(posts);

        }

        [HttpGet]
        [Route("count")]
        public ActionResult CountPosts()
        {
            return Ok(PostService.Count(x => x.UserId == LoggedInUserId));
        }

        #region Comment Code
        [HttpGet]
        [Route("{postId}/comments")]
        public ActionResult GetComment(int postId, [FromQuery]int skip, [FromQuery]int take)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }


            var comments = CommentService.GetMany(x => x.UserId == LoggedInUserId && x.PostId == postId, skip, take);

            return Ok(comments);

        }

        #endregion

        [HttpPost]
        [Route("{postId}/like")]
        public ActionResult CreatePostLike(int postId)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }

            var postLike = new PostLike
            {
                UserId = LoggedInUserId.Value,
                PostId = postId,


            };
            var postLikeId = PostLikeService.Create(postLike);

            return Ok(new CreateResponse { Id = postLikeId });

        }



        [HttpGet]
        [Route("{postId}/like")]
        public ActionResult GetLikes(int postId)
        {
            return Ok(PostLikeService.Count(x => x.PostId == postId));

        }

        [HttpDelete]
        [Route("{postId}/like")]
        public ActionResult Delete(int postId)
        {
            PostLikeService.Delete(x => x.PostId == postId && x.UserId == LoggedInUserId);

            return Ok();

        }

        //Route("{postId}/like") POST
        //Route("{postId}/like") GET
        //Route("{postId}/like") DELETE
    }
}
