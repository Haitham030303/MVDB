namespace MVDB.Models
{
    public class SearchResultViewModel
    {
        public IList<Movie>? Movies { get; set; }
        public IList<Person>? People { get; set; }
        public ResultType ResultType { get; set; }
    }

    public enum ResultType
    {
        DisplayMovies,
        DisplayPeople
    }
}
