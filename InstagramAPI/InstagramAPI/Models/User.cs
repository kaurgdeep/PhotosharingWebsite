using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Models
{
    public class User : IdentityUser
    {

        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public string PasswordSalt { get; set; }


        public DateTime Created { get; set; }
    } 
}
