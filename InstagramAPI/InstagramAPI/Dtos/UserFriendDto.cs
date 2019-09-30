using InstagramAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class UserFriendDto
    {
        public int UserFriendId { get; set; }
        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public virtual User Friend { get; set; }


        public User UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
