using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Services;

namespace MovieCharactersAPI.Controllers
{
    // autmomappern ska vara här
    // tar emot requst kollar i services gör det som finns där och sen "mappar" 

    [ApiController]
    [Route("api/v1/[controller]")]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        public FranchiseController(IFranchiseService franchiseService) 
        {
            _franchiseService = franchiseService;        
        }

        // Gett all Franchises 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetFranchises()
        {
            return Ok(await _franchiseService.GetAllFranchises());
        }
    }
}
