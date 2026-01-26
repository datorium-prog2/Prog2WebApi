using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prog2WebApi.Data;
using Prog2WebApi.Models;
using Prog2WebApi.Models.Requests;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
            var allPosts = _db.Posts
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.UserId,
                    LikeCount = p.Likes.Count
                })
                .ToList();
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

        // atribūts authorize. lietotājam jābūt autorizētam
        // ja ir šis atribūts - no tokena var izgūt info par lietotāju
        [Authorize]
        [HttpPost]
        public IActionResult CreatePost(PostRequest request)
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var post = Post.From(request, userId);
            _db.Posts.Add(post);
            _db.SaveChanges();
            return Ok(post);
        }

        [Authorize]
        [HttpPost("{id:int}/like")]
        public IActionResult Like(int id)
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

            var existingLike = _db.Likes.FirstOrDefault(
                l => l.UserId == userId && l.PostId == id);

            if (existingLike == null)
            {
                var like = new Like
                {
                    PostId = id,
                    UserId = userId,
                };
                _db.Likes.Add(like);
                _db.SaveChanges();

                return Ok(new { msg = "Like added." });
            }

            _db.Likes.Remove(existingLike);
            _db.SaveChanges();

            return Ok(new { msg = "Like removed." });
        }
    }
}
