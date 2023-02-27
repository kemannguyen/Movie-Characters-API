using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models.DTOS
{
    public class CharacterDTO
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Alias { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; }

        [MaxLength(100)]
        public string Picture { get; set; }
        public List<string> Movies { get; set; }
    }
}
