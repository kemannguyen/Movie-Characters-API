using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;
using MovieCharactersAPI.Services;
using System.Net.Mime;

namespace MovieCharactersAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;
        public FranchiseController(IFranchiseService franchiseService, IMapper mapper) 
        {
            _franchiseService = franchiseService;        
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the Franchises entities
        /// </summary>
        /// <returns>List of franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDTO>>> GetFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDTO>>(await _franchiseService.GetAllFranchises()));
        }

        /// <summary>
        /// Get franchise entity by id
        /// </summary>
        /// <param name="id">Franchise id</param>
        /// <returns>A franchise resource</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDTO>> getFranchiseById(int id)
        {
            try
            {
               return Ok(_mapper.Map<FranchiseDTO>(await _franchiseService.GetFranchiseById(id)));
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        /// <summary>
        /// Get all movies entities that are connected to a specific franchise entity. 
        /// </summary>
        /// <param name="id">Franchise id</param>
        /// <returns>A list of all movies in the franchise</returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetCharactersInMovie(int id)
        {
            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(await _franchiseService.GetAllMoviesInFranchise(id)));
        }

        /// <summary>
        /// Get all characters that are connected to a specific franchise entity
        /// </summary>
        /// <param name="id">Franchise id</param>
        /// <returns>A list of all characters in the franchise</returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharactersInFranchise(int id)
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDTO>>(await _franchiseService.GetAllCharactersInFranchise(id)));
        }
        /// <summary>
        /// Create a new franchise entity 
        /// </summary>
        /// <param name="createFranchaseDTO">Franchise object to add</param>
        /// <returns>The franchise resource</returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(CreateFranchaseDTO createFranchaseDTO)
        {
            var franchase = _mapper.Map<Franchise>(createFranchaseDTO);
            await _franchiseService.AddFranchise(franchase);

            return CreatedAtAction(nameof(getFranchiseById), new {id = franchase.Id}, franchase);
        }

        /// <summary>
        /// Delete a specific franchise entity based on id
        /// </summary>
        /// <param name="id">The id of the resource to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteFranchise(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }

        
        /// <summary>
        /// Update a franchise entity
        /// </summary>
        /// <param name="id">The id for the franchise</param>
        /// <param name="franchise">The values to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchises(int id, CreateFranchaseDTO franchise)
        {
            try
            {
                await _franchiseService.UpdateFranchise(_mapper.Map<Franchise>(franchise));
            }
            catch (Exception ex) 
            { 
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,    
                });
            }

            return NoContent();
        }
        
        /// <summary>
        /// Add movie to the franchise entity.
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <param name="movieIds">Array of the movieId's to add</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> AddMovieToFranchise(int id, params int[] movieIds)
        {
            try
            {
                await _franchiseService.AddMovieToFranchise(id, movieIds);
            }
            catch (Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }

            return NoContent();
        }
    }
}
