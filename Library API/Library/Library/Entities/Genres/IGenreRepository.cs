namespace Library.Models.Genres;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllGenresAsync();
    Task<Genre> GetGenreByIdAsync(string Id);
    Task CreateGenreAsync(Genre genre);
    Task UpdateGenreAsync(Genre genre);
    Task DeleteGenreAsync(string Id);
    Task SaveChangesAsync();
}