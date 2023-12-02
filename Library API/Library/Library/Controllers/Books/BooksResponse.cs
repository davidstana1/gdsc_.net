using System.Reflection.Metadata.Ecma335;

namespace Library.Controllers;

public class BooksResponse
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    public string Author { get; set; }

    public int Pages { get; set; }

    public string GenreId { get; set; }

    public string GenreName { get; set; }
}