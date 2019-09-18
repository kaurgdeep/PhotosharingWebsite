using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramAPI.Dtos;
using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using InstagramAPI.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstagramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(Constants.InstagramServerCorsPolicy)]
    public class CommentsController : InstagramBaseController
    {
        private IEntityService<User> UserService;
        private IEntityService<Post> PostService;
        private IEntityService<Comment> CommentService;

        public CommentsController(IEntityService<User> userService, IEntityService<Post> postService, IEntityService<Comment> commentService)
        {
            UserService = userService;
            PostService = postService;
            CommentService = commentService;

        }
        [HttpPost]
        [Route("")]
        public ActionResult CreateComment([FromBody] CommentDto commentDto)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }

            var comment = new Comment
            {
                UserId = LoggedInUserId.Value,
                CommentId = commentDto.CommentId,
                CommentText = commentDto.CommentText,
                CreatedAt = DateTime.UtcNow,


            };
            var commentId = CommentService.Create(comment);

            return Ok(new CreateResponse { Id = commentId });

        }

    }
}