using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public string PostText { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

       
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}
