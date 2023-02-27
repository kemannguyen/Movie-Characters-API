using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class MovieController: ControllerBase
    {

        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET: api/Guitars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(await _movieService.GetAllMovies());
        }

        // GET: api/Guitars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return await _movieService.GetMovieById(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

    }
}
