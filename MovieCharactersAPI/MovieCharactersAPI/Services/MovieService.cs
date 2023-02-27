﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<Movie> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
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

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if(movie == null)
            {
                throw new Exception("not found" + id);
            }

            return movie;
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

        public async Task<Movie> UpdateCharactersInMovie(Movie movie, ICollection<Character> characters)
        {
            var foundMovie = await _context.Movies.AnyAsync(x => x.Id==movie.Id);
            if(!foundMovie)
            {
                throw new Exception("not found" + movie.Id);
            }
            //updating the characters?
            movie.Characters = characters;
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
