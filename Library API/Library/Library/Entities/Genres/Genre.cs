namespace Library.Models.Genres;

public class Genre : Entity
{
    public string GenreName { get; set; }

    public List<Books> Books { get; set; }

    public Genre(string genreName)
    {
        GenreName = genreName;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name can't be empty ! ");

        name = name.Replace(" ", " ");
        GenreName = name;
    }
    
}