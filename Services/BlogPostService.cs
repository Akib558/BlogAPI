using BlogAPI.DTOs;
using BlogAPI.Repositories;

namespace BlogAPI.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;

        public BlogPostService(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public Task<BlogPostDto?> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task AddAsync(BlogPostDto blogPost)
        {
            return _repository.AddAsync(blogPost);
        }

        public Task UpdateAsync(BlogPostDto blogPost)
        {
            return _repository.UpdateAsync(blogPost);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }

}
