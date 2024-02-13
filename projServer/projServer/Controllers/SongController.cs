using Microsoft.AspNetCore.Mvc;
using projServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // GET: api/<SongController>
        [HttpGet("GetSongsNames")]
        public IEnumerable<string> GetSongsNames()
        {
            return Song.GetSongsNames();
        }

        // GET api/<SongController>/5
        [HttpGet("GetNsongs/{n}")]
        public IEnumerable<Song> GetNsongs(int n)
        {
            return Song.GetN(n); ;
        }
        
        [HttpGet("SearchSongs/{key}")]
        public IEnumerable<Song> SearchSongs(string key)
        {
            return Song.Search(key); ;
        } 

         [HttpGet("GetSongsByArtistName/{name}")]
        public IEnumerable<Song> GetSongsByArtistName(string name)
        {
            return Song.GetSongsByArtist(name); ;
        } 
        
        [HttpGet("SearchSongsByArtist/{key}")]
        public IEnumerable<Song> SearchSongsByArtist(string key)
        {
            return Song.SearchByArtist(key); ;
        } 

        [HttpGet("GetSongById/{id}")]
        public Song GetSongById(int id)
        {
            return Song.GetSong(id); ;
        }

        // POST api/<SongController>
        [HttpPost("UpdateSong")]
        public bool UpdateSong([FromBody] Song s)
        {
            return s.UpdateSongInfo();
        }

        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
