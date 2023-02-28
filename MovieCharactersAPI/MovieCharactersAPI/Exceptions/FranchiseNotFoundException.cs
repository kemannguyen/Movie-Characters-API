namespace MovieCharactersAPI.Exceptions
{
    public class FranchiseNotFoundException : Exception
    {
        public FranchiseNotFoundException(string message) : base(message) { }
    }
}
