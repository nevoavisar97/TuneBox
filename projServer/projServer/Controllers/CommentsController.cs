using Microsoft.AspNetCore.Mvc;
using projServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        // GET: api/<CommentsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CommentsController>/5
        [HttpGet("{song_id}")]
        public IEnumerable<Comment> Get(int song_id)
        {
            Comment c = new Comment();
            return c.GetCommentsBySong(song_id);
        }

        // POST api/<CommentsController>
        [HttpPost]
        public bool Post([FromBody] Comment c)
        {
            return c.Insert();
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
