using Microsoft.AspNetCore.Mvc;
using Prog2WebApi.Data;
using Prog2WebApi.Models;
using Prog2WebApi.Models.Requests;

namespace Prog2WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        // šeit notiek AppDbContext injekcija
        public UsersController(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpPost("/register")]
        public IActionResult CreateUser(UserRequest request)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            var user = new User()
            {
                Username = request.Username,
                Password = request.Password
            };
            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(user);
        }

        [HttpPost("/login")]
        public IActionResult LoginUser(UserRequest request)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUser == null)
            {
                return NotFound("User with this username does not exist.");
            }

            if (existingUser.Password != request.Password)
            {
                return Unauthorized("Incorrect password");
            }

            return Ok(existingUser.Id);
        }
    }
}
