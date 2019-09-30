using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        [StringLength(128, ErrorMessage = "{0} character limit of {1} exceeded.")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
        public string Password { get; set; }
      
        public string FirstName { get; set; }

        public string LastName { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Password is not complex enough) // TODO: Implement Password complexity check
        //    {
        //        yield return new ValidationResult("Password is not complex enough");
        //    }
        //}
    }
}
