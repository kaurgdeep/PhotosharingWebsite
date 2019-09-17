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
                var sid = (Constants.UserId);
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

                var emailAddressFromClaims = (Constants.EmailAddress);
                if (emailAddressFromClaims == null)
                {
                    return null;
                }
                emailAddress = emailAddressFromClaims;

                return emailAddress;
            }
        }
    }
}