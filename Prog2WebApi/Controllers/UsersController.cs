using Microsoft.AspNetCore.Mvc;
using Prog2WebApi.Data;
using Prog2WebApi.Models;

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

        [HttpPost]
        public IActionResult CreateUser()
        {
        }
    }
}
