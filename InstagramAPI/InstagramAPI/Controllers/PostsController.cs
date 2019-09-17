using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramAPI.Dtos;
using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using InstagramAPI.Responses;
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

        public PostsController(IEntityService<User> userService, IEntityService<Post> postService, IEntityService<Comment> commentService)
        {
            UserService = userService;
            PostService = postService;
            CommentService = commentService;

        }
        [HttpPost]
        [Route("createPost")]
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
                CreatedAt = DateTime.UtcNow,


            };
            var postId = PostService.Create(post);

            return Ok(new CreateResponse { Id = postId });

        }
    }
}
