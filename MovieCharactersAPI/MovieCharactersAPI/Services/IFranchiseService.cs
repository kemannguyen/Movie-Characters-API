using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Services
{
    // alla metoder som behövs, i franchise 
    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();

        Task<Franchise> GetFranchiseById(int id);

        Task<Franchise> AddFranchise(Franchise franchise);

        Task<Franchise> UpdateFranchise(Franchise franchise);

        Task DeleteFranchise(int id);
    }
}
