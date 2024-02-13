using Microsoft.AspNetCore.Mvc;
using projServer.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user = new User();
            return user.Read();
        }

         [HttpGet("GetUserFavorites/{id}")]
        public IEnumerable<Song> GetUserFavorites(int id)
        {
            User u = new User();
            return u.GetUserFavoriteSongs(id);
        }

        [HttpGet("GetUserInfo")]
        public IEnumerable<string> GetUserInfo()
        {
            User u = new User();
            return u.GetUserInfo();
        }
         [HttpGet("GetArtistInfo")]
        public IEnumerable<string> GetArtistInfo()
        {
            User u = new User();
            return u.GetArtistInfo();
        }
         [HttpGet("GetSongInfo")]
        public IEnumerable<string> GetSongInfo()
        {
            User u = new User();
            return u.GetSongInfo();
        }

        [HttpGet("CheckUserFavorite/{user}/{song}")]
        public bool CheckUserFavorite(int user,int song)
        {
            User u = new User();
            return u.CheckFav(user,song);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User u = new User();
            return u.GetUserById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("AddRemoveFav/{user}/{song}")]
        public bool AddRemoveFav(int user, int song)
        {
            User u = new User();
            return u.Fav(user, song);
        }

        // POST api/<UsersController>
        [HttpPost]
        public int Post([FromBody] User u)
        {
            return u.Registration();
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("UserLogIn/{email}/{password}")]
        public ActionResult UserLogIn(string email, string password)
        {
            try
            {
                User u = new User();
                return Ok(u.LogIn(email, password));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] User user)
        {
            return user.Update(id);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
