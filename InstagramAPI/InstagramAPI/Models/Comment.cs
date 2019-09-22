using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<CommentLike> CommentLikes { get; set; }

        public Comment()
        {
            CommentLikes = new List<CommentLike>();
        }




    }
}
