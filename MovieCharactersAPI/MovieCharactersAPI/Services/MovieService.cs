using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Context;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieCharactersDbContext _context;

        public MovieService(MovieCharactersDbContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(x => x.Franchise).Include(x => x.Characters).ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies.Include(x => x.Franchise).Include(x=> x.Characters).FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                throw new Exception("not found" + id);
            }
            //var franchise = await _context.Franchises.FindAsync(movie.FranchiseId);
            
            return movie;
        }
        
        public async Task<Movie> AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new Exception("make a custom exception"+id);
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var foundMovie = await _context.Movies.AnyAsync(x => x.Id == movie.Id);
            if(!foundMovie)
            {
                throw new Exception("not found" + movie.Id);
            }
            _context.Entry(movie).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return movie;
        }

        //also needs to change characterMovie table
        public async Task<Movie> UpdateCharactersInMovie(int movieId, params int [] ids)
        {
            var foundMovie = await _context.Movies.FindAsync(movieId);
            if(foundMovie == null)
            {
                throw new Exception("not found" + movieId);
            }
            var includedCharacters = new List<Character>();
            foreach(var i in ids)
            {
                var tempChar = await _context.Characters.FindAsync(i);
                if (tempChar != null)
                {
                    includedCharacters.Add(tempChar);
                }
                else
                {
                    throw new Exception("not found" + i);
                }
            }

            foundMovie.Characters= includedCharacters;

            _context.Entry(foundMovie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return foundMovie;
        }

        //get all characters of a movie
    }
}
