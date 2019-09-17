using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramAPI
{
    public class Constants
    {
        public const string InstagramServerCorsPolicy = "InstagramWebServerCorsPolicy";

        // Configuration keys
        public static string JWTSecurityKey = "SecurityKey";

        public static string DbConnection = "DbConnection";

        // Claims
        public static string EmailAddress = "EmailAddress";
        public static string UserId = "UserId";
    }
}
