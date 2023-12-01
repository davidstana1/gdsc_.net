namespace Library.Models;

public class Books
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public int Pages { get; set; }

    private Books()
    {
    }

    public static Books Create(string name, string author,int pages)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty ! ");

        if (string.IsNullOrWhiteSpace((author)))
            throw new Exception("You need to have an author to the book ! ");

        if (pages < 50)
            throw new Exception("A book has to have more than 50 pages ! ");

        return new Books
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Author = author,
            Pages = pages
        };
    }

    public void SetName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty ! ");

        name = name.Replace(" ", " ");
        Name = name;
    }

    public void SetAuthor(string author)
    {
        if(string.IsNullOrWhiteSpace(author))
            throw new Exception("You need to have an author to the book ! ");

        author = author.Replace(" ", " ");
        Author = author;
    }

    public void SetPages(int pages)
    {
        if (pages < 50)
            throw new Exception("A book has to have more than 50 pages ! ");

        Pages = pages;
    }
    
}