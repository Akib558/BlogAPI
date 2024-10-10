namespace BlogAPI.DTOs
{
    public class BlogPostDto
    {
        public Guid BlogGuid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        public DateTime EditedDate { get; set; }
    }

}
