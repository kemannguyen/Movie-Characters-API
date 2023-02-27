using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public interface ICharacterService
    {
        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Character>> GetAllCharacters();

        /// <summary>
        /// Gets a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Character> GetCharacterById(int id);

        /// <summary>
        /// Updates an existing character
        /// </summary>
        /// <param name="character">New character</param>
        /// <returns></returns>
        Task<Character> UpdateCharacter(Character character);

        /// <summary>
        /// Deletes an existing character
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns></returns>
        Task DeleteCharacter(int id);

        /// <summary>
        /// Adds a new character
        /// </summary>
        /// <param name="character">Character to add</param>
        /// <returns></returns>
        Task<Character> AddCharacter(Character character);
    }
}
