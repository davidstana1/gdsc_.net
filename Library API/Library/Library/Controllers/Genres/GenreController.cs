using Library.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Library.Models.Genres;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers.Genres;

[Route("genres")]
[ApiController]

public class GenreController : ControllerBase
{
    private readonly AppDbContext _context;

    public GenreController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponse>>> GetGenre()
    {
        var genres = await _context.Genres
            .Include(g => g.Books)
            .ToListAsync();

        return Ok(genres.Select(g => new GenreResponse
        {
            Id = g.Id,
            Name = g.GenreName,
            Books = g.Books.Select(b => new BooksResponse
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Pages = b.Pages,
                GenreId = b.Id
            }).ToList()
        }));
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre([FromBody] GenreRequest request)
    {
        var genre = new Genre(request.name);

        _context.Add(genre);
        await _context.SaveChangesAsync();

        return Ok(genre);
    }
    
    
    
}