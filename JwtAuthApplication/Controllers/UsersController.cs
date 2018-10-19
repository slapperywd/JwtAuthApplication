
using System.Collections.Generic;


namespace JwtAuthApplication.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using JwtAuthApplication.Constants;
    using JwtAuthApplication.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private List<User> users = new List<User>
                                        {
                                            new User { Name = "Test1", Password = "Test1" },
                                            new User { Name = "Ivan", Password = "Ivanov" }
                                        };
                                         

        // POST api/values
        [AllowAnonymous]
        [HttpPost("/token")]
        public IActionResult Post([FromBody] User userBody)
        {
            var user = users.FirstOrDefault(x => x.Name == userBody.Name && x.Password == userBody.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            string token = this.GenerateToken(user);

            var response = new { access_token = token, username = user.Name };
            return Ok(response);
        }
          


        private string GenerateToken(User user)
        {
            // create JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthConstants.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject = new ClaimsIdentity(new Claim[]
                                                                           {
                                                                               new Claim(ClaimTypes.Name, user.Name)
                                                                           }),
                                          Expires = DateTime.UtcNow.AddMinutes(AuthConstants.TokenLifeTime),
                                          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                                      };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

