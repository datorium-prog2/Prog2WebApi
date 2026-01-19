namespace Prog2WebApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }

        public static Post From(PostDto dto)
        {
            return new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}
