using System.Runtime.CompilerServices;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _context;

    public BooksRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Books>> GetAllBooksAsync()
    {
        return await _context.BooksEnumerable.ToListAsync();
    }

    public async Task<Books> GetBookByIdAsync(string id)
    {
        return await _context.BooksEnumerable.FindAsync(id);
    }

    public async Task CreateBookAsync(Books books)
    {
        _context.Add(books);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Books books)
    {
        _context.Update(books);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveBookAsync(string id)
    {
        var book = await _context.BooksEnumerable.FindAsync(id);
        if (book != null)
        {
            _context.BooksEnumerable.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Books>> GetAllBooksWithGenreAsync()
    {
        return await _context.BooksEnumerable
            .Include(b => b.Genre)
            .ToListAsync();
    }
    
}