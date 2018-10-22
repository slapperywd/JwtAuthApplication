namespace JwtAuthApplication.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using JwtAuthApplication.Models;
    using JwtAuthApplication.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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

            var tokenInfo = JwtTokenGeneratorService.GenerateToken(user);
            tokenInfo.UserName = user.Name;

            return Ok(tokenInfo);
        }
    }
}

