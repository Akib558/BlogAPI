using BlogAPI.DTOs;

namespace BlogAPI.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPostDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogPostDto>> GetAllAsync();
        Task AddAsync(BlogPostDto blogPost);
        Task UpdateAsync(BlogPostDto blogPost);
        Task DeleteAsync(int id);
    }

}
