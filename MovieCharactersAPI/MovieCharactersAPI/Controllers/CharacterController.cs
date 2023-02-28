using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;
using MovieCharactersAPI.Services;
using System.Net.Mime;

namespace MovieCharactersAPI.Controllers
{
    [Route("/v1/[controller]")]
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

        /// <summary>
        /// Gets all characters entities
        /// </summary>
        /// <returns>All Characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDTO>>(await _characterService.GetAllCharacters()));
        }

        /// <summary>
        /// Gets character entity by Id
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Character entity</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacterById(int id)
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

        /// <summary>
        /// Creates a character entity
        /// </summary>
        /// <param name="createCharacterDTO">Character to create</param>
        /// <returns>Created character entity</returns>
        [HttpPost]
        public async Task<ActionResult<CreateCharacterDTO>> CreateCharacter(CreateCharacterDTO createCharacterDTO)
        {
            var character = _mapper.Map<Character>(createCharacterDTO);
            await _characterService.AddCharacter(character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
        }

        /// <summary>
        /// Deletes a character entity
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns>No content or Not found message</returns>

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

        /// <summary>
        /// Updates a character entity
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <param name="character">Updated characters</param>
        /// <returns>Updated character</returns>
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
