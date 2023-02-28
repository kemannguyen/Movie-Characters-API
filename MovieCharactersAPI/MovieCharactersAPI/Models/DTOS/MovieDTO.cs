using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models.DTOS
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }       
        public string Genre { get; set;}
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        public List<string> Characters { get; set; }
        public int FranchiseId { get; set; }
        public string Franchise { get; set; }
    }
}
