namespace Library.Models;

public interface IBooksRepository
{
    Task<List<Books>> GetAllBooksAsync();

    Task<Books> GetBookByIdAsync(string id);

    Task CreateBookAsync(Books books);

    Task UpdateBookAsync(Books books);

    Task RemoveBookAsync(string id);

    Task SaveChangesAsync();

    Task<List<Books>> GetAllBooksWithGenreAsync();
}