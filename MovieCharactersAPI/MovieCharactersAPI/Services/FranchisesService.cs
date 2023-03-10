using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Context;
using MovieCharactersAPI.Exceptions;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public class FranchisesService : IFranchiseService
    {
        private readonly MovieCharactersDbContext _context;

        public FranchisesService(MovieCharactersDbContext context)
        {
            _context = context;
        }

        public async Task<Franchise> AddFranchise(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            return franchise;
        }

        public async Task<Franchise> AddMovieToFranchise(int id, params int[] movieIds)
        {
            var franchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            var movies = new List<Movie>();

            if (franchise == null)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }

            foreach (int movieId in movieIds)
            {
                var movie = await _context.Movies.Include(x => x.Characters).FirstAsync(x => movieId == x.Id);
                if (franchise.Movies.Any(x => x.Id == movieId))
                    continue;
                if (movie == null)
                {
                    throw new MovieNotFoundException("movie not found");
                }
                movies.Add(movie);
            }

            foreach (Movie movie in movies)
            {
                franchise.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }

            return franchise;
        }

        public async Task DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (franchise == null)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }

            foreach (var movie in franchise.Movies)
            {
                movie.Franchise = null;
                movie.FranchiseId = null;
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharactersInFranchise(int id)
        {
            var foundFranchise = await _context.Franchises.Include(x => x.Movies).ThenInclude(x => x.Characters).FirstOrDefaultAsync(x => x.Id == id);

            if (foundFranchise == null)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }
            var characterList = new List<Character>();
            foreach (Movie m in foundFranchise.Movies)
            {
                foreach (Character c in m.Characters)
                {
                    if (!characterList.Contains(c))
                    {
                        characterList.Add(c);
                    }

                }
            }
            return characterList;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _context.Franchises.Include(x => x.Movies).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesInFranchise(int id)
        {
            var foundFranchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (foundFranchise == null)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }

            return foundFranchise.Movies.ToList();
        }

        public async Task<Franchise> GetFranchiseById(int id)
        {
            var franchise = await _context.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            if (franchise == null)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }

            return franchise;
        }

        public async Task<Franchise> UpdateFranchise(Franchise franchise)
        {
            var foundFranchise = await _context.Franchises.AnyAsync(x => x.Id == franchise.Id);
            if (!foundFranchise)
            {
                throw new FranchiseNotFoundException("franchise not found");
            }
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return franchise;
        }


    }
}
