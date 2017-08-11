using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code.Models;
using System.Net;
using System.Net.Mail;

namespace Code.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly PostsContext _context;

        public PostsController(PostsContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPost()
        {
            return _context.Post;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);


            if (Post == null)
            {
                return NotFound();
            }

            return Ok(Post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post Post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost(Post Post)
        {
            //SendEmail();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Post.Add(Post);


            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPost", new { id = Post.PostId }, Post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            if (Post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(Post);
            await _context.SaveChangesAsync();

            return Ok(Post);
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }

        public string SendEmail ()
        {
            MailMessage emailMessage = new MailMessage();
            SmtpClient client = new SmtpClient("eximc.nam.dow.com");
            client.Credentials = new NetworkCredential("username", "password");
            string ResourceName = "MMunroe@dow.com";

            emailMessage.From = new MailAddress(ResourceName);
            emailMessage.To.Add(ResourceName);
            emailMessage.Subject = "Another New Post";
            emailMessage.Body = "Here is a new email message";

            client.Send(emailMessage);
         
            return "";
        }
   
    }

}