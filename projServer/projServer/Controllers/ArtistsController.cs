using Microsoft.AspNetCore.Mvc;
using projServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        // GET: api/<ArtistsController>
        [HttpGet]
        public IEnumerable<string> GetArtitsNames()
        {
            return Artist.GetArtitsNames();
        }

        // GET api/<ArtistsController>/5
        [HttpGet("GetNartists/{n}")]
        public IEnumerable<Artist> GetNartists(int n)
        {
            return Artist.GetN(n); ;
        }
         [HttpGet("GetAllArtists")]
        public IEnumerable<Artist> GetAllArtists()
        {
            return Artist.GetArtists(); ;
        }

        // GET api/<ArtistsController>/5
        [HttpGet("GetArtist/{name}")]
        public Artist GetArtist(string name)
        {
            return Artist.GetArtistByName(name); ;
        }

        // POST api/<ArtistsController>
        [HttpPost]
        public bool Post([FromBody] Artist a)
        {
            return a.InsertArtist();
        }

        // PUT api/<ArtistsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArtistsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
