using BlogAPI.DTOs;
using BlogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _service;

        public BlogPostController(IBlogPostService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var blogPost = await _service.GetByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogPosts = await _service.GetAllAsync();
            return Ok(blogPosts);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BlogPostDto blogPost)
        {
            await _service.AddAsync(blogPost);

            return CreatedAtAction(nameof(GetById), new { id = blogPost.BlogGuid }, blogPost);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BlogPostDto blogPost)
        {
            await _service.UpdateAsync(blogPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
