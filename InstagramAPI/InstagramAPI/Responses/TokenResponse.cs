﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI.Responses
{
    public class TokenResponse : SuccessResponse
    {
        public string Token { get; set; }
    }
}
