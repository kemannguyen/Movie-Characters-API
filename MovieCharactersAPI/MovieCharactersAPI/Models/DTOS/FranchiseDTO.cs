namespace MovieCharactersAPI.Models.DTOS
{
    // bara det vi vill visa anvädaren för att undvika konfliker eller gömma info
    public class FranchiseDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<string> Movies { get; set; }
    }
}
