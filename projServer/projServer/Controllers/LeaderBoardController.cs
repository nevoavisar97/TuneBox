using Microsoft.AspNetCore.Mvc;
using projServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        // GET: api/<LeaderBoardController>
        [HttpGet("GetL")]
        public IEnumerable<LeaderBoard> GetL()
        {
            return LeaderBoard.Get();
        }

        // GET api/<LeaderBoardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LeaderBoardController>
        [HttpPost]
        public bool Post([FromBody] LeaderBoard l)
        {
            return l.Insert();
        }
        // PUT api/<LeaderBoardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LeaderBoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
