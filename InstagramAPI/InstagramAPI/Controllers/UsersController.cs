using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InstagramAPI.Dtos;
using InstagramAPI.Interfaces.Services;
using InstagramAPI.Models;
using InstagramAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InstagramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : InstagramBaseController
    {

        private IConfiguration Configuration;
        private IEntityService<User> UserService;

        

        public UsersController(IConfiguration configuration, IEntityService<User> userService)
        {
            Configuration = configuration;
            UserService = userService;
        }

        // POST api/values
        [HttpPost]
        [Route("register")]
        public ActionResult<TokenResponse> Register([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                // todo: Hash the password before saving
                var user = new User
                {
                    EmailAddress = userDto.EmailAddress.ToLower(),
                    PasswordHash = userDto.Password,
                    
                    Created = DateTime.UtcNow
                };
                var userId = UserService.Create(user);
                user.UserId = userId;
                var token = CreateToken(user);

                return Ok(new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                // TODO: Log the exception details 
                var messages = new List<string>();
                while (ex != null)
                {
                    messages.Add(ex.Message);
                    ex = ex.InnerException;
                }

                // Note: These are 2 different ways to return an error result
                // JsonResult is better because we can send back error information in JSON (just like success)
                // which makes handling any HTTP calls to API uniform for the clients of the API (like the Web)
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Error = string.Join("\r\n", messages) });
                // Note: In production, don't return any more details - return just the status
                return StatusCode(StatusCodes.Status500InternalServerError, string.Join("\r\n", messages));
            }
        }

        [HttpPost]
        [Route("log-in")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginUserDto userDto)
        {
            // todo: hash the input password and check with passwordhash from database
            var user = UserService.Get(u =>
                u.EmailAddress == userDto.EmailAddress.ToLower() &&
                u.PasswordHash == userDto.Password);
                
            if (user == null)
            {
                return BadRequest("Could not verify username and password");
            }

            var token = CreateToken(user);

            return Ok(new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        private JwtSecurityToken CreateToken(User user)
        {
            var claims = new[] {
                new Claim(Constants.EmailAddress, user.EmailAddress),
                new Claim(Constants.UserId, user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JWTSecurityKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Instagram.com",
                audience: "Instagram.com",
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return token;
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public ActionResult Me()
        {
            return Ok(new MeResponse
            {
                UserId = LoggedInUserId,
                EmailAddress = LoggedInEmailAddress,
                
            });
        }

    }
}