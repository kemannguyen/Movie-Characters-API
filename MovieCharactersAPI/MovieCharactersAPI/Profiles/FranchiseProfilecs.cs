using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;

namespace MovieCharactersAPI.Profiles
{
    public class FranchiseProfilecs : Profile
    {
        public FranchiseProfilecs()
        {
            CreateMap<CreateFranchaseDTO, Franchise>();
            CreateMap<Franchise, FranchiseDTO>().ForMember(dto => dto.Movies, options =>
            options.MapFrom(franchiseDoman => franchiseDoman.Movies.Select(movie => $"api/v1/movie/{movie.Id}")));
        }
    }
}
