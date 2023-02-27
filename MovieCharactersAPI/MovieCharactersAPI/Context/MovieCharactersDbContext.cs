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
                new Character { Id = 1, Name = "Leonardo Di Caprio", Alias = "", Gender = "Male", Picture = "Google.com" },
                new Character { Id = 2, Name = "Robert De Niro", Alias = "", Gender = "Male", Picture = "Google.com" },
                new Character { Id = 3, Name = "Sandra Bullock", Alias = "", Gender = "Female", Picture = "Google.com" },
                new Character { Id = 4, Name = "Adam Sandler", Alias = "", Gender = "Male", Picture = "Google.com" }
                );

            modelBuilder.Entity<Franchise>().HasData(
                new Franchise { Id = 1, Name = "Disney", Description = "Disney" },
                new Franchise { Id = 2, Name = "MCU", Description = "MCU" }
               );
            modelBuilder.Entity<Movie>().HasData(
              new Movie { Id = 1, MovieTitle = "Happy Gilmore", Genre = "Comedy", ReleaseYear = 1932, Director = "Adam Sandler", Picture = "google.com", Trailer = "Youtube.com", FranchiseId = 1 },
              new Movie { Id = 2, MovieTitle = "Inception", Genre = "Thriller", ReleaseYear = 1932, Director = "??", Picture = "google.com", Trailer = "Youtube.com", FranchiseId = 1 },
              new Movie { Id = 3, MovieTitle = "The Irishman", Genre = "Action", ReleaseYear = 1932, Director = "James Beanie", Picture = "google.com", Trailer = "Youtube.com", FranchiseId = 2 }
              );

            modelBuilder.Entity<Movie>()
                 .HasMany(p => p.Characters)
                 .WithMany(m => m.Movies)
                 .UsingEntity<Dictionary<string, object>>(
                     "CharacterMovie",
                     r => r.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                     l => l.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                     je =>
                     {
                         je.HasKey("CharactersId", "MoviesId");
                         je.HasData(
                             new { CharactersId = 1, MoviesId = 1 },
                             new { CharactersId = 1, MoviesId = 2 },
                             new { CharactersId = 2, MoviesId = 3 },
                             new { CharactersId = 3, MoviesId = 3 },
                             new { CharactersId = 4, MoviesId = 1 },
                             new { CharactersId = 4, MoviesId = 2 }
                         );
                     });


        }
    }
}
