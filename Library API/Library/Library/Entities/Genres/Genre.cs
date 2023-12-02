namespace Library.Models.Genres;

public class Genre : Entity
{
    public string GenreName { get; set; }

    public List<Books> Books { get; set; }

    public Genre(string genreName)
    {
        GenreName = genreName;
    }
}