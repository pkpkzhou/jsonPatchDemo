using JsonPatchSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonPatchSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context) => _context = context;

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs(string keyword)
        {
            var query = _context.Blogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(e => e.Url.Contains(keyword));
            }

            return await query.ToListAsync();
        }

        // GET: api/Blogs/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<ActionResult<Blog>> CreateBlog([FromBody] Blog blog)
        {
            _context.Blogs.Add(blog);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogId }, blog);
        }

        // PUT: api/Blogs/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Blogs.Any(e => e.BlogId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Blogs/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Blogs/1
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchBlog(int id, [FromBody] JsonPatchDocument<Blog> patch)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            patch.ApplyTo(blog, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Blogs.Any(e => e.BlogId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(blog);
        }
    }
}