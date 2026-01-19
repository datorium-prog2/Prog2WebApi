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
            var user = new User()
            {
                Username = request.Username,
                Password = request.Password
            };
            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(user);
        }
    }
}
