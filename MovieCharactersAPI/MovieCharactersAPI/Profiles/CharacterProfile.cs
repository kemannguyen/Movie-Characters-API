using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.DTOS;

namespace MovieCharactersAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile() 
        {
            CreateMap<Character, CharacterDTO>()
                .ForMember(dto => dto.Movies, options =>
                {
                    options.MapFrom(c => c.Movies.Select(m => $"api/v1/movie/{m.Id}").ToList());
                });
        }
    }
}
