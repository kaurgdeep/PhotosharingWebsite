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
        private IEntityService<CommentLike> CommentLikeService;
        private IEntityService<PostLike> PostLikeService;

        public CommentsController(IEntityService<User> userService, IEntityService<Post> postService, IEntityService<PostLike> postLikeService, IEntityService<Comment> commentService, IEntityService<CommentLike> commentLikeService)
        {
            UserService = userService;
            PostService = postService;
            CommentService = commentService;
            PostLikeService = postLikeService;
            CommentLikeService = commentLikeService;

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
               // CommentId = commentDto.CommentId,
                CommentText = commentDto.CommentText,
                CreatedAt = DateTime.UtcNow,
                PostId = commentDto.PostId


            };
            var commentId = CommentService.Create(comment);

            return Ok(new CreateResponse { Id = commentId });

        }

        

        [HttpGet]
        [Route("count")]
        public ActionResult CountComments()
        {
            return Ok(CommentService.Count(x => x.UserId == LoggedInUserId));
        }

        [HttpPost]
        [Route("{commentId}/like")]
        public ActionResult CreateCommentLike(int commentId)
        {
            if (LoggedInUserId == null)
            {
                return BadRequest("Invalid user id");
            }

            var commentLike = new CommentLike
            {
                UserId = LoggedInUserId.Value,
                CommentId = commentId,


            };
            var commentLikeId = CommentLikeService.Create(commentLike);

            return Ok(new CreateResponse { Id = commentLikeId });

        }

        [HttpGet]
        [Route("{commenttId}/like")]
        public ActionResult GetLikes(int commentId)
        {
            return Ok(CommentLikeService.Count(x => x.CommentId == commentId));

        }


        [HttpDelete]
        [Route("{commentId}/like")]
        public ActionResult Delete(int commentId)
        {
            CommentLikeService.Delete(x => x.CommentId == commentId && x.UserId == LoggedInUserId);

            return Ok();

        }

    }
}