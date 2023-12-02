using Library.Models;
using Library.Models.Genres;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]

public class BooksController : ControllerBase
{
  private readonly AppDbContext _dbContext;

  public BooksController(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }
    
  [HttpGet(Name = "GetAllBooks")]
  public async Task<ActionResult<IEnumerable<BooksResponse>>> GetBooks()
  {
    var books = await _dbContext.BooksEnumerable
      .Include(b => b.Genre).ToListAsync();
    
    
    
    return Ok(books.Select(b => new BooksResponse
    {
      Id=b.Id,
      Name=b.Name,
      Author = b.Author,
      Pages=b.Pages,
      GenreId = b.Genre.Id,
      GenreName = b.Genre.GenreName
    }));
  }

  [HttpGet("{Id}")]
  public async Task<ActionResult> GetBooks(string Id)
  {
    var book = await _dbContext.BooksEnumerable
      .Where(books => books.Id == Id)
      .OrderBy(book => book.Name)
      .FirstOrDefaultAsync();

    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    return Ok(book);
  }

  [HttpPost]
  public async Task<ActionResult> CreateBook([FromBody] BooksRequest booksRequest)
  {

    var bookGenre = await _dbContext.Genres
      .FirstOrDefaultAsync(g => g.Id == booksRequest.GenreId);

    Books book = null;
    try
    {
      book = await Books.CreateAsync(
        bookGenre,
        booksRequest.Name,
        booksRequest.Author,
        booksRequest.Pages
      );
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    _dbContext.Add(book);
    
   await _dbContext.SaveChangesAsync();
    
    return Ok(new BooksResponse
    {
      Id=book.Id,
      Name=book.Name,
      Author = book.Author,
      Pages=book.Pages,
      GenreId = bookGenre.Id
    });
  }

  [HttpDelete("{Id}")]
  public async Task<ActionResult> DeleteBook(string Id)
  {
    var book = _dbContext.BooksEnumerable.FirstOrDefault(b => b.Id == b.Id);

    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    _dbContext.Remove(book);
    _dbContext.SaveChangesAsync();

    return Ok($"Book with Id : {Id} was removed ! ");
  }

  [HttpPatch("{Id}")]
  public async Task<ActionResult> ChangeName(string Id, [FromBody] string name)
  {
    var book = _dbContext.BooksEnumerable.FirstOrDefault(b => b.Id == Id);
    
    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    try
    {
      book.SetName(name);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    _dbContext.SaveChangesAsync();
    
    return Ok(book);
  }

  [HttpPut("{Id}")]
  public async Task<ActionResult> ChangeBook(string Id, [FromBody] BooksRequest booksRequest)
  {
    var book = _dbContext.BooksEnumerable.FirstOrDefault(b => b.Id == Id);
    
    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    try
    {
      book.SetName(booksRequest.Name);
      book.SetAuthor(booksRequest.Author);
      book.SetPages(booksRequest.Pages);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    _dbContext.SaveChangesAsync();
    
    return Ok(book);
  }
  
}