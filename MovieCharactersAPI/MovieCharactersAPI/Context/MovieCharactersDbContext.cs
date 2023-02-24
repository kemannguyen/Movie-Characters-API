using Microsoft.EntityFrameworkCore;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Context
{
    public class MovieCharactersDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        public MovieCharactersDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1,Name = "Leonardo Di Caprio", Alias = "", Gender = "Male", Picture = "Google.com" },
                new Character { Id = 2, Name = "Robert De Niro", Alias = "", Gender = "Male", Picture = "Google.com" },
                new Character { Id = 3, Name = "Sandra Bullock", Alias = "", Gender = "Female", Picture = "Google.com" },
                new Character { Id = 4, Name = "Adam Sandler", Alias = "", Gender = "Male", Picture = "Google.com" }
                );
            modelBuilder.Entity<Franchise>().HasData(
               new Franchise { Id = 1, Name = "Disney", Description = "Disney" },
               new Franchise { Id = 2, Name = "The MCU", Description = "Superheroes" }
               );
            modelBuilder.Entity<Movie>().HasData(
              new Movie { Id = 1, MovieTitle = "Disney", Genre = "Disney", ReleaseYear = 1932, Director = "James Cameron", Picture = "google.com", Trailer = "Youtube.com", Franchise = {Id = 1} },
              new Movie { Id = 2, MovieTitle = "Disney", Genre = "Disney", ReleaseYear = 1932, Director = "James Cameron", Picture = "google.com", Trailer = "Youtube.com", Franchise = { Id = 1 } },
              new Movie { Id = 3, MovieTitle = "Disney", Genre = "Disney", ReleaseYear = 1932, Director = "James Cameron", Picture = "google.com", Trailer = "Youtube.com", Franchise = { Id = 2 }}
              );


        }
    }
}
