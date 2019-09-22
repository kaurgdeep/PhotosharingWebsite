using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class CommentLikeDto
    {
        public int CommentLikeId { get; set; }
        public int CommentId { get; set; }
        public User UserId { get; set; }
        public User User { get; set; }
    }
}
