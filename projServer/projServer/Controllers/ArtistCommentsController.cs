using Microsoft.AspNetCore.Mvc;
using projServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistCommentsController : ControllerBase
    {
        // GET: api/<ArtistCommentsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArtistCommentsController>/5
        [HttpGet("{artist}")]
        public IEnumerable<ArtistComment> Get(string artist)
        {
            ArtistComment c = new ArtistComment();
            return c.GetCommentsByArtist(artist);
        }

        // POST api/<ArtistCommentsController>
        [HttpPost]
        public bool Post([FromBody] ArtistComment c)
        {
            return c.Insert();
        }

        // PUT api/<ArtistCommentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArtistCommentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
