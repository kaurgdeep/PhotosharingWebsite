using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Like> Likes { get; set; }
    }
}
