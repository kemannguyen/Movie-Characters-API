using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
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
        public FranchiseController(IFranchiseService franchiseService) 
        {
            _franchiseService = franchiseService;        
        }

        /// <summary>
        /// GEt all the Franchises resources
        /// </summary>
        /// <returns>List of franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetFranchises()
        {
            return Ok(await _franchiseService.GetAllFranchises());
        }

        // GET: api/franchises/2
        /// <summary>
        /// Get one specific franchise based on an unique identifier
        /// </summary>
        /// <param name="Id">Franchise id</param>
        /// <returns>A franchise resource</returns>
        [HttpGet("Id")]
        public async Task<ActionResult<Franchise>> getFranchise(int Id)
        {
            try
            {
                return await _franchiseService.GetFranchiseById(Id);
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
        public async Task<ActionResult<Franchise>> PostFranchise(Franchise franchise)
        {
            return CreatedAtAction("GetFranchises", new { id = franchise.Id }, await _franchiseService.AddFranchise(franchise));
        }

        /// <summary>
        /// Delete a specific resource from database based on a unique identifier
        /// </summary>
        /// <param name="Id">The id of the resource to delete</param>
        /// <returns></returns>
        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteFranchise(int Id)
        {
            try
            {
                await _franchiseService.DeleteFranchise(Id);
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
        [HttpPut("Id")]
        public async Task<IActionResult> PutFranchises(int id, Franchise franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            try
            {
                await _franchiseService.UpdateFranchise(franchise);
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
