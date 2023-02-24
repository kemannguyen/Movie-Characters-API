using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models
{
    public class Character
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

        public ICollection<Movie> Movies { get; set;}
    }
}
