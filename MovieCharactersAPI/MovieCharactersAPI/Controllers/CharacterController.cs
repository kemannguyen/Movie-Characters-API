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
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDTO>>(await _characterService.GetAllCharacters()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            try
            {
                return _mapper.Map<CharacterDTO>(await _characterService.GetCharacterById(id));
            }
            catch(Exception ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> PostGuitar(Character character)
        {
            return CreatedAtAction("GetGuitar", new { id =character.Id }, _mapper.Map < Character, CharacterDTO > (await _characterService.AddCharacter(character)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteCharacter(id);
            }
            catch(Exception ex)
            {
                return NotFound(new ProblemDetails { Detail = ex.Message });
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
                return BadRequest();

            try
            {
                await _characterService.UpdateCharacter(character);
            }
            catch(Exception ex)
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
