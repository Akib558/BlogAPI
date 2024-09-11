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

        public async Task<BlogPostDto?> GetByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Where(bp => bp.Id == id)
                .Select(bp => new BlogPostDto
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content,
                    PublishedDate = bp.PublishedDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            return await _context.BlogPosts.Select(bp => new BlogPostDto
            {
                Id = bp.Id,
                Title = bp.Title,
                Content = bp.Content,
                PublishedDate = bp.PublishedDate
            }).ToListAsync();
        }

        public async Task AddAsync(BlogPostDto blogPostDto)
        {
            if (blogPostDto == null)
            {
                throw new ArgumentNullException(nameof(blogPostDto));
            }

            // Create a new BlogPost entity from the DTO
            var blogPost = new BlogPost
            {
                //Id = blogPostDto.Id,  // Assuming Id is auto-generated; typically, you should not set this
                Title = blogPostDto.Title,
                Content = blogPostDto.Content,
                PublishedDate = blogPostDto.PublishedDate
            };

            _context.BlogPosts.Add(blogPost);  // Add the entity to the context
            await _context.SaveChangesAsync();  // Save changes to the database
        }


        public async Task UpdateAsync(BlogPostDto blogPostDto)
        {
            if (blogPostDto == null)
            {
                throw new ArgumentNullException(nameof(blogPostDto));
            }

            // Fetch the existing entity from the database
            var blogPost = await _context.BlogPosts.FindAsync(blogPostDto.Id);

            if (blogPost == null)
            {
                throw new InvalidOperationException("BlogPost not found");
            }

            // Map properties from DTO to entity
            blogPost.Title = blogPostDto.Title;
            blogPost.Content = blogPostDto.Content;
            blogPost.PublishedDate = blogPostDto.PublishedDate;

            // Mark the entity as modified (this is not strictly necessary in this case)
            _context.BlogPosts.Update(blogPost);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }


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
