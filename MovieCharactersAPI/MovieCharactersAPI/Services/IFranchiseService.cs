using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    public interface IFranchiseService
    {
        /// <summary>
        /// Get all resources 
        /// </summary>
        /// <returns>A list of resources</returns>
        Task<IEnumerable<Franchise>> GetAllFranchises();

        /// <summary>
        /// Get a specific resource base of a unique identifier 
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <returns>Returns that specific resource</returns>
        Task<Franchise> GetFranchiseById(int id);

        /// <summary>
        /// Add a resource
        /// </summary>
        /// <param name="franchise">The object to add</param>
        /// <returns></returns>
        Task<Franchise> AddFranchise(Franchise franchise);

        /// <summary>
        /// Update a resource 
        /// </summary>
        /// <param name="franchise">The resource to update</param>
        /// <returns>The updated resource</returns>
        Task<Franchise> UpdateFranchise(Franchise franchise);

        /// <summary>
        /// Delete a resource 
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <returns></returns>
        Task DeleteFranchise(int id);
    }
}
