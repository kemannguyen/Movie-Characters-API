using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Context;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public class CharacterService : ICharacterService
    {
        public readonly MovieCharactersDbContext _context;

        public CharacterService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<Character> AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<Character> AddMovieToCharacter(int id, int movieId)
        {
            var character = await GetCharacterById(id);
            if (character is null)
                throw new CharacterNotFoundException("Character not found");

            var movie = await _context.Movies.Include(x => x.Characters).FirstOrDefaultAsync(x => x.Id == movieId);
            if (movie is null)
                throw new MovieNotFoundException("Movie not found");

            character.Movies.Add(movie);
            movie.Characters.Add(character);
            await _context.SaveChangesAsync();

            return character;
        }

        public async Task DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character is null)
                throw new CharacterNotFoundException("Character Not Found");
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _context.Characters.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = await _context.Characters.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            if (character is null)
                throw new CharacterNotFoundException("Character Not Found");
            return character;
        }

        public async Task<Character> UpdateCharacter(Character character)
        {
            var foundCharacter = await _context.Characters.AnyAsync(x => x.Id == character.Id);
            if (!foundCharacter)
                throw new CharacterNotFoundException("Character Not Found");
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return character;

        }
    }
}
