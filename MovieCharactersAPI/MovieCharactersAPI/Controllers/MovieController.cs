using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        // GET: api/Guitars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(await _movieService.GetAllMovies()));
            //return Ok(await _movieService.GetAllMovies());
        }

        // GET: api/Guitars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDTO>(await _movieService.GetMovieById(id)));
                //return await _movieService.GetMovieById(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(CreateMovieDTO createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);
            await _movieService.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovie(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult<Movie>> UpdateCharactersInMovie(int movieId, params int[] ids)
        {

            try
            {
                await _movieService.UpdateCharactersInMovie(movieId, ids);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> PutMovie(int movieId, UpdateMovieDTO updateMovie)
        {
            if(movieId != updateMovie.Id)
            {
                return BadRequest();
            }
            try
            {
                var movie = _mapper.Map<Movie>(updateMovie);
                await _movieService.UpdateMovie(movie);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
            return NoContent();
        }
    }
}
