using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogPostDbContext _context;

        public BlogPostRepository(BlogPostDbContext context)
        {
            _context = context;
        }

        // Fetch a blog post by its BlogGuid (Guid)
        public async Task<BlogPostDto?> GetByIdAsync(Guid BlogGuid)
        {
            return await _context.BlogPosts
                .Where(bp => bp.BlogGuid == BlogGuid)
                .Select(bp => new BlogPostDto
                {
                    BlogGuid = bp.BlogGuid,
                    Title = bp.Title,
                    Content = bp.Content,
                    PublishedDate = bp.PublishedDate
                })
                .FirstOrDefaultAsync();
        }

        // Fetch all blog posts
        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            return await _context.BlogPosts
                .Select(bp => new BlogPostDto
                {
                    BlogGuid = bp.BlogGuid,
                    Title = bp.Title,
                    Content = bp.Content,
                    PublishedDate = bp.PublishedDate
                })
                .ToListAsync();
        }

        // Add a new blog post
        public async Task AddAsync(BlogPostDto blogPostDto)
        {
            if (blogPostDto == null)
            {
                throw new ArgumentNullException(nameof(blogPostDto));
            }

            // Create a new BlogPost entity from the DTO
            var blogPost = new BlogPost
            {
                BlogGuid = blogPostDto.BlogGuid, // Ensure the BlogGuid is properly set
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                PublishedDate = blogPostDto.PublishedDate
            };

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
        }

        // Update an existing blog post
        public async Task UpdateAsync(BlogPostDto blogPostDto)
        {
            if (blogPostDto == null)
            {
                throw new ArgumentNullException(nameof(blogPostDto));
            }

            // Fetch the existing entity
            var blogPost = await _context.BlogPosts.FindAsync(blogPostDto.BlogGuid);

            if (blogPost == null)
            {
                throw new InvalidOperationException("BlogPost not found");
            }

            // Update properties
            blogPost.Title = blogPostDto.Title;
            blogPost.Content = blogPostDto.Content;
            blogPost.PublishedDate = blogPostDto.PublishedDate;

            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
        }

        // Delete a blog post by its Id
        public async Task DeleteAsync(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
        }
    }
}
