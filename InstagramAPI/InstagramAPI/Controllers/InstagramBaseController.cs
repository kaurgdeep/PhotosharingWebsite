using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstagramAPI.Controllers
{
    
    [ApiController]
    public abstract class InstagramBaseController : ControllerBase
    {
        // backing variable.
        private int? userId;
        protected int? LoggedInUserId
        {
            get
            {
                if (userId != null)
                {
                    return userId;
                }

                // userId == null
                var sid = GetClaim(Constants.UserId);
                if (sid == null)
                {
                    return null;
                }
                if (int.TryParse(sid, out int id))
                {
                    userId = id;
                }

                return userId;
            }
        }

        private string emailAddress;
        protected string LoggedInEmailAddress
        {
            get
            {
                if (emailAddress != null)
                {
                    return emailAddress;
                }

                var emailAddressFromClaims = GetClaim(Constants.EmailAddress);
                if (emailAddressFromClaims == null)
                {
                    return null;
                }
                emailAddress = emailAddressFromClaims;

                return emailAddress;
            }
        }

        private string firstName;
        protected string LoggedInFirstName
        {
            get
            {
                if (firstName != null)
                {
                    return firstName;
                }

                var firstNameFromClaims = GetClaim(Constants.FirstName);
                if (firstNameFromClaims == null)
                {
                    return null;
                }
                firstName = firstNameFromClaims;

                return firstName;
            }
        }

        private string lastName;
        protected string LoggedInLastName
        {
            get
            {
                if (lastName != null)
                {
                    return lastName;
                }

                var lastNameFromClaims = GetClaim(Constants.LastName);
                if (lastNameFromClaims == null)
                {
                    return null;
                }
                lastName = lastNameFromClaims;

                return lastName;
            }
        }

        private string GetClaim(string claimType)
        {
            return User?.Claims?.Where(c => c.Type == claimType)?.FirstOrDefault()?.Value;
        }
    }
}