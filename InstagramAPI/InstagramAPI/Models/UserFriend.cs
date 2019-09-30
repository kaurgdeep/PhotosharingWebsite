using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Models
{
    public class UserFriend
    {
        public int UserFriendId { get; set; }
        public int FriendId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
