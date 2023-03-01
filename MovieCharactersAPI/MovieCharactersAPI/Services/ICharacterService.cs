using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public interface ICharacterService
    {
        /// <summary>
        /// Gets all characters
        /// </summary>
        /// <returns>Enumerable of Character entities</returns>
        Task<IEnumerable<Character>> GetAllCharacters();

        /// <summary>
        /// Gets a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character entity</returns>
        Task<Character> GetCharacterById(int id);

        /// <summary>
        /// Updates an existing character
        /// </summary>
        /// <param name="character">New character</param>
        /// <returns>Updated character entity</returns>
        Task<Character> UpdateCharacter(Character character);

        /// <summary>
        /// Deletes an existing character
        /// </summary>
        /// <param name="id">Id of character</param>
        /// <returns>Deletes a character entity</returns>
        Task DeleteCharacter(int id);

        /// <summary>
        /// Adds a new character
        /// </summary>
        /// <param name="character">Character to add</param>
        /// <returns>Created character entity</returns>
        Task<Character> AddCharacter(Character character);

        /// <summary>
        /// Adds an existing movie to the character, and the character to the movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <returns>Character entity</returns>
        Task<Character> AddMovieToCharacter(int id, int movieId);
    }
}
