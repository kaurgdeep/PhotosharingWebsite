using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class UserFriendDto
    {
        public int UserFriendId { get; set; }
        public int FriendId { get; set; }
        public User UserId { get; set; }
        public User User { get; set; }
    }
}
