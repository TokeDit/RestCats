using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTcats.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTcats.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private CatsRepositoryList _repo;

        public CatsController(CatsRepositoryList repo)
        {
            _repo = repo;
        }

        // GET: api/<CatsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Cat>> Get([FromQuery]int? minimumweight, [FromQuery]int? maximumweight)
        {
            if (minimumweight > maximumweight)
            {
                return BadRequest();
            }
            IEnumerable<Cat> result = _repo.GetAllCats(minimumweight, maximumweight);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // GET api/<CatsController>/5
        [HttpGet("{id}")]
        public Cat? Get(int id)
        {
            return _repo.GetCatById(id);
        }

        // POST api/<CatsController>
        [HttpPost]
        public Cat Post([FromBody] Cat newCat)
        {
            return _repo.AddCat(newCat);
        }

        // PUT api/<CatsController>/5
        [HttpPut("{id}")]
        public Cat? Put(int id, [FromBody] Cat value)
        {
            return _repo.UpdateCat(id, value);
        }

        // DELETE api/<CatsController>/5
        [HttpDelete("{id}")]
        public Cat? Delete(int id)
        {
            return _repo.RemoveCat(id);
        }
        [HttpOptions]
        public string Options()
        {
            return "Det virker";
        }
    }
}   
