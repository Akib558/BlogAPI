using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class BlogPost
    {
        [Key]
        public Guid BlogGuid { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        public DateTime EditedDate { get; set; }
        public Guid UserGuid { get; set; } // foreign key

        public BlogUser BlogCreator { get; set; } = null!;
        public ICollection<BlogAuthor> BlogAuthors { get; set; } = new List<BlogAuthor>();


    }

}
