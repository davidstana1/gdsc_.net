namespace Library.Controllers.Genres;

public class GenreResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public List<BooksResponse> Books { get; set; }
    
}