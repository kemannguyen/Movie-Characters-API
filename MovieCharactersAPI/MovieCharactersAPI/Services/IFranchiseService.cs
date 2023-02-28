using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public interface IFranchiseService
    {
        /// <summary>
        /// Get all franchises 
        /// </summary>
        /// <returns>A list of franchises</returns>
        Task<IEnumerable<Franchise>> GetAllFranchises();

        /// <summary>
        /// Get a specific franchise based of id
        /// </summary>
        /// <param name="id">The franchise id</param>
        /// <returns>Returns that specific franchise</returns>
        Task<Franchise> GetFranchiseById(int id);

        /// <summary>
        /// Add a franchise
        /// </summary>
        /// <param name="franchise">The object to add</param>
        /// <returns></returns>
        Task<Franchise> AddFranchise(Franchise franchise);

        /// <summary>
        /// Update a franchise 
        /// </summary>
        /// <param name="franchise">The franchise to update</param>
        /// <returns>The updated franchise</returns>
        Task<Franchise> UpdateFranchise(Franchise franchise);

        /// <summary>
        /// Delete a franchise 
        /// </summary>
        /// <param name="id">The franchise id</param>
        /// <returns></returns>
        Task DeleteFranchise(int id);

        /// <summary>
        /// Add a movie to a franchise 
        /// </summary>
        /// <param name="id">Id of the franchise</param>
        /// <param name="movieIds">An array of movieId's to add to the franchise</param>
        /// <returns></returns>
        Task<Franchise> AddMovieToFranchise(int id, params int[] movieIds);

        /// <summary>
        /// Get all movies connected to a franchise
        /// </summary>
        /// <param name="id">The franchise id</param>
        /// <returns>A list of the movies</returns>
        Task<IEnumerable<Movie>> GetAllMoviesInFranchise(int id);

        /// <summary>
        /// Get all the characters connected to a franchise 
        /// </summary>
        /// <param name="id">The id of the franchise</param>
        /// <returns>A list of all the characters</returns>
        Task<IEnumerable<Character>> GetAllCharactersInFranchise(int id);

    }
}
