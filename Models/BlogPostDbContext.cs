using BlogAPI;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

public class BlogPostDbContext : DbContext
{
    public BlogPostDbContext(DbContextOptions<BlogPostDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<BlogUser> BlogUsers { get; set; }
    public DbSet<BlogAuthor> BlogAuthors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogAuthor>()
    .HasOne(ba => ba.BlogUser)
    .WithMany(bu => bu.BlogAuthors)
    .HasForeignKey(ba => ba.UserGuid)
    .OnDelete(DeleteBehavior.Restrict); // Set to Restrict to prevent cascading delete

        modelBuilder.Entity<BlogAuthor>()
            .HasOne(ba => ba.BlogPost)
            .WithMany(bp => bp.BlogAuthors)
            .HasForeignKey(ba => ba.BlogGuid)
            .OnDelete(DeleteBehavior.Cascade); // This can remain Cascade
        modelBuilder.Entity<BlogPost>().HasOne(bp => bp.BlogCreator)
                                        .WithMany(bc => bc.BlogPosts)
                                        .HasForeignKey(bp => bp.UserGuid)
                                        .IsRequired();

        modelBuilder.Entity<BlogAuthor>()
            .HasKey(ba => new { ba.UserGuid, ba.BlogGuid });

        modelBuilder.Entity<BlogAuthor>()
            .HasOne(ba => ba.BlogUser)
            .WithMany(bu => bu.BlogAuthors)
            .HasForeignKey(ba => ba.UserGuid);

        modelBuilder.Entity<BlogAuthor>()
            .HasOne(ba => ba.BlogPost)
            .WithMany(bp => bp.BlogAuthors)
            .HasForeignKey(ba => ba.BlogGuid);

    }



    public override int SaveChanges()
    {
        // Automatically set PublishedDate for new BlogPosts
        foreach (var entry in ChangeTracker.Entries<BlogPost>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.PublishedDate = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatically set PublishedDate for new BlogPosts
        foreach (var entry in ChangeTracker.Entries<BlogPost>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.PublishedDate = DateTime.Now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
