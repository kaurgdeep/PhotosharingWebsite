using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Models
{
    public class CommentLike
    {
        public int CommentLikeId { get; set; }

        [Required]
        public int CommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
