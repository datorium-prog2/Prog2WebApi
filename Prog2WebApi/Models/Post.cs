using Prog2WebApi.Models.Requests;

namespace Prog2WebApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        public static Post From(PostRequest dto, int userId)
        {
            return new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
        }
    }
}
