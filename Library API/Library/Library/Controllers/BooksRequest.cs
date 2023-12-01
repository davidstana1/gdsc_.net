namespace Library.Controllers;

public record BooksRequest(string Name, string Author, int Pages);

public class BooksRequest1
{
    public string Name { get; set; }

    public string Author { get; set; }

    public int Pages { get; set; }
}