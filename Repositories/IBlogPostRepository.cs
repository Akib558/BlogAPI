using BlogAPI.DTOs;

namespace BlogAPI.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPostDto?> GetByIdAsync(int id);
        Task<IEnumerable<BlogPostDto>> GetAllAsync();
        Task AddAsync(BlogPostDto blogPost);
        Task UpdateAsync(BlogPostDto blogPost);
        Task DeleteAsync(int id);
    }

}
