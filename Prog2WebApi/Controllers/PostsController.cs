using Microsoft.AspNetCore.Mvc;
using Prog2WebApi.Data;
using Prog2WebApi.Models;
using Prog2WebApi.Models.Requests;

namespace Prog2WebApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;

        // šeit notiek AppDbContext injekcija
        public PostsController(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            List<Post> allPosts = _db.Posts.ToList();
            return Ok(allPosts);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPostById(int id)
        {
            var post = _db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public IActionResult CreatePost(PostRequest request)
        {
            var post = Post.From(request);
            _db.Posts.Add(post);
            _db.SaveChanges();
            return Ok(post);
        }

        [HttpGet("user/{id:int}")]
        public IActionResult GetPostsByUser(int id)
        {
            var userPosts = _db.Posts.Where(p => p.UserId == id).ToList();
            return Ok(userPosts);
        }
    }
}
