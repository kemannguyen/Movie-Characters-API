using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// finds all movies
        /// </summary>
        /// <returns> returns all movies </returns>
        Task<IEnumerable<Movie>> GetAllMovies();

        /// <summary>
        /// Finds a movie based on its id
        /// </summary>
        /// <param name="id"> movie id </param>
        /// <returns> found movie entity </returns>
        Task<Movie> GetMovieById(int id);

        /// <summary>
        /// Add a movie
        /// </summary>
        /// <param name="movie"> Movie object </param>
        /// <returns> Added movie entity </returns>
        Task<Movie> AddMovie(Movie movie);

        /// <summary>
        /// Delete a selected movie
        /// </summary>
        /// <param name="id"> id of movie</param>
        /// <returns></returns>
        Task DeleteMovie(int id);

        /// <summary>
        /// Update the info of a certain moive
        /// </summary>
        /// <param name="movie"> object of the selected movie</param>
        /// <returns></returns>
        Task<Movie> UpdateMovie(Movie movie);

        /// <summary>
        /// Adds a new character to a movie if it doesn't exist // replaces the existing characters in the movie with a new one
        /// </summary>
        /// <param name="movieID"> id of movie </param>
        /// <param name="ids"> ids of characters </param>
        ///<returns> The Updated resource </returns>
        Task<Movie> AddCharactersInMovie(int movieID, params int[] ids);

        /// <summary>
        /// gets all characters of a certain movie
        /// </summary>
        /// <param name="id"> id of movie</param>
        /// <returns> IEnumerable of Character objects </returns>
        Task<IEnumerable<Character>> GetAllCharactersInMovie(int id);
    }
}
