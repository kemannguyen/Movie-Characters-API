using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCharactersAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string MovieTitle { get; set; }

        [MaxLength(80)]
        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        [MaxLength(40)]
        public string Director { get; set; }

        [MaxLength(100)]
        public string Picture { get; set; }

        [MaxLength(100)]
        public string Trailer { get; set; }
        public ICollection<Character> Characters { get; set; }
        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }

    }
}
