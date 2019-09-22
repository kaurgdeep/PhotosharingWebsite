using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class PostLikeDto
    {
        public int PostLikeId { get; set; }
        public int PostId { get; set; }
        public User UserId { get; set; }
        public User User { get; set; }
    }
}
