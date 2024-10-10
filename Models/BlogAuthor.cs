using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class BlogAuthor
    {
        [Key]
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public Guid BlogGuid { get; set; }

        [ForeignKey("UserGuid")]
        public BlogUser BlogUser { get; set; } = null!;

        [ForeignKey("BlogGuid")]
        public BlogPost BlogPost { get; set; } = null!;
    }

}
