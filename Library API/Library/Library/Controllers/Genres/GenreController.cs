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
    private readonly IGenreRepository _genreRepository;
    
    public GenreController(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponse>>> GetGenre()
    {
        var genres = await _genreRepository.GetAllGenresAsync();

        var genreResponses = genres.Select(g => new GenreResponse
        {
            Id = g.Id,
            Name = g.GenreName,
            Books = g.Books.Select(b => new BooksResponse
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Pages = b.Pages,
                GenreId = b.Genre.Id , 
                GenreName = b.Genre.GenreName
            }).ToList() 
        }).ToList();

        return Ok(genreResponses);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult> GetGenreId(string Id)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(Id);

        if (genre is null)
            return NotFound($"Genre with id : {Id} doesn't exist ! ");

        var genreResponse = new GenreResponse
        {
            Id = genre.Id,
            Name = genre.GenreName,
            Books = genre.Books.Select(b => new BooksResponse
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Pages = b.Pages,
                GenreId = b.Genre.Id,
                GenreName = b.Genre.GenreName
            }).ToList() 
        };

        return Ok(genreResponse);
    }
    
    [HttpPost]
    public async Task<ActionResult<Genre>> CreateGenre([FromBody] GenreRequest request)
    {
        var genre = new Genre(request.name);

        await _genreRepository.CreateGenreAsync(genre);
        await _genreRepository.SaveChangesAsync();
    
    return Ok(genre);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteGenre(string Id)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(Id);

        if (genre is null)
            return NotFound($"Genre with id {Id} does not exist ! ");
        
        await _genreRepository.DeleteGenreAsync(Id);
        await _genreRepository.SaveChangesAsync();

        return Ok($"Genre with id : {Id} was deleted ! ");
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateGenre(string Id, [FromBody] string name)
    {
        var genre = await _genreRepository.GetGenreByIdAsync(Id);
        
        if(genre is null)
            return NotFound($"Genre with id {Id} does not exist ! ");

        try
        {
            genre.SetName(name);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        genre.SetName(name);
        await _genreRepository.SaveChangesAsync();
        
        var facultyResponse = new GenreResponse
        {
            Id = genre.Id,
            Name = genre.GenreName,
            Books = genre.Books.Select(b => new BooksResponse
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Pages = b.Pages,
                GenreId = b.Genre.Id,
                GenreName = b.Genre.GenreName
            }).ToList()
        };

        return Ok(facultyResponse);
    }
    
}