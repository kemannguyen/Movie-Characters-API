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

        /// <summary>
        /// get all movies
        /// </summary>
        /// <returns> all movies </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(await _movieService.GetAllMovies()));
            //return Ok(await _movieService.GetAllMovies());
        }

        /// <summary>
        /// get a movie 
        /// </summary>
        /// <param name="id"> id of movie </param>
        /// <returns> specific movie </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDTO>(await _movieService.GetMovieById(id)));
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Get all characters of a movie 
        /// </summary>
        /// <param name="id"> id of the movie </param>
        /// <returns> all characters of specific movie </returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharactersInMovie(int id)
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDTO>>(await _movieService.GetAllCharactersInMovie(id)));
        }

        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="createMovieDTO"> input of new movie </param>
        /// <returns> movie </returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(CreateMovieDTO createMovieDTO)
        {
            var movie = _mapper.Map<Movie>(createMovieDTO);
            await _movieService.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// deletes a movie based on id
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <returns></returns>
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

        /// <summary>
        /// Add character to a movie
        /// </summary>
        /// <param name="movieId"> id of movie </param>
        /// <param name="ids"> id of characters </param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult<Movie>> AddCharactersInMovie(int movieId, params int[] ids)
        {
            try
            {
                await _movieService.AddCharactersInMovie(movieId, ids);
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

        /// <summary>
        /// Update movie
        /// </summary>
        /// <param name="movieId"> id of movie </param>
        /// <param name="updateMovie"> new info for updated movie </param>
        /// <returns></returns>
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
