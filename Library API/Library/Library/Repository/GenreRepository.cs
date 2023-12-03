using Library.Models.Genres;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres
            .Include(g => g.Books)
            .ToListAsync();
    }

    public async Task<Genre> GetGenreByIdAsync(string Id)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == Id);
    }

    public async Task CreateGenreAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(string Id)
    {
        var genre = await _context.Genres.FindAsync(Id);
        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        _context.SaveChangesAsync();
    }
}