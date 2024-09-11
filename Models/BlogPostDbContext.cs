using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

public class BlogPostDbContext : DbContext
{
    public BlogPostDbContext(DbContextOptions<BlogPostDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
}
