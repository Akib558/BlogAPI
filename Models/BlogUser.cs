using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogAPI.Models;

namespace BlogAPI;

public class BlogUser
{
    [Key]
    public Guid UserGuid { get; set; } = Guid.NewGuid();
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    public ICollection<BlogAuthor> BlogAuthors { get; set; } = new List<BlogAuthor>();
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

}
