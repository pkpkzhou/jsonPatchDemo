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
    public class PostsController : ControllerBase
    {
        private readonly BloggingContext _context;

        public PostsController(BloggingContext context) => _context = context;

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(int? blogId, string title)
        {
            var query = _context.Posts.AsQueryable();

            if (blogId.HasValue)
            {
                query = query.Where(e => e.BlogId == blogId.Value);
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(e => e.Title.Contains(title));
            }

            return await query.ToListAsync();
        }

        // GET: api/Posts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            _context.Posts.Add(post);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
        }

        // PUT: api/Posts/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Posts.Any(e => e.PostId == id))
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

        // DELETE: api/Posts/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Posts/1
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patch)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            patch.ApplyTo(post, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}