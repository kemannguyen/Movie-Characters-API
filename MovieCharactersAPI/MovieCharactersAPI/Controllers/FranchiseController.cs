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
        /// GEt all the Franchises resources
        /// </summary>
        /// <returns>List of franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDTO>>> GetFranchises()
        {
            // return Ok(await _franchiseService.GetAllFranchises());
            return Ok(_mapper.Map<IEnumerable<FranchiseDTO>>(await _franchiseService.GetAllFranchises()));
        }

        /// <summary>
        /// Get one specific franchise based on an unique identifier
        /// </summary>
        /// <param name="Id">Franchise id</param>
        /// <returns>A franchise resource</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDTO>> getFranchiseById(int id)
        {
            try
            {
               // return await _franchiseService.GetFranchiseById(Id);
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
        /// Add a new franchise to the database 
        /// </summary>
        /// <param name="franchise">Franchises object to add</param>
        /// <returns>The franchises resource </returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(CreateFranchaseDTO createFranchaseDTO)
        {
            //return CreatedAtAction("GetFranchises", new { id = franchise.Id }, await _franchiseService.AddFranchise(franchise));
            var franchase = _mapper.Map<Franchise>(createFranchaseDTO);
            await _franchiseService.AddFranchise(franchase);

            return CreatedAtAction(nameof(getFranchiseById), new {id = franchase.Id}, franchase);
        }

        /// <summary>
        /// Delete a specific resource from database based on a unique identifier
        /// </summary>
        /// <param name="Id">The id of the resource to delete</param>
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
        /// update a resource
        /// </summary>
        /// <param name="id">the id for the resource to update</param>
        /// <param name="franchise">Check if it's same as the one you are updating</param>
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> AdddMovieToFranchase(int id, params int[] movieIds)
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
