using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;

namespace MovieCharactersAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<UpdateMovieDTO, Movie>();
            CreateMap<CreateMovieDTO, Movie>();
            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.Characters, options =>
                options.MapFrom(movieDomain => movieDomain.Characters.Select(character => $"api/v1/character/{character.Id}").ToList()))
                .ForMember(dto => dto.Franchise, options =>
                options.MapFrom(movieDomain => $"api/v1/franchise/{movieDomain.FranchiseId}"));

        }
    }
}
