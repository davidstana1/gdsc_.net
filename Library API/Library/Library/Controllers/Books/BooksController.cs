using Library.Models;
using Library.Models.Genres;
using Library.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]

public class BooksController : ControllerBase
{
  
  private readonly IBooksRepository _booksRepository;
  private readonly IGenreRepository _genreRepository;
  public BooksController(IBooksRepository booksRepository,IGenreRepository genreRepository)
  {
    _genreRepository = genreRepository;
    _booksRepository = booksRepository;
  }
    
  [HttpGet(Name = "GetAllBooks")]
  public async Task<ActionResult<IEnumerable<BooksResponse>>> GetBooks()
  {
    var books = await _booksRepository.GetAllBooksWithGenreAsync();
    
    var response= books.Select(books => new
    {
      Id=books.Id,
      Name=books.Name,
      Author = books.Author,
      Pages=books.Pages,
      GenreId = books.Genre.Id,
      GenreName = books.Genre.GenreName
    });

    return Ok(response);
  }

  [HttpGet("{Id}")]
  public async Task<ActionResult> GetBooks(string Id)
  {
    var book = await _booksRepository.GetBookByIdAsync(Id);
    var books = await _booksRepository.GetAllBooksWithGenreAsync();
    
    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");
  
    var response= new BooksResponse
    {
      Id=book.Id,
      Name=book.Name,
      Author = book.Author,
      Pages=book.Pages,
      GenreId = book.Genre.Id,
      GenreName = book.Genre.GenreName
    };

    return Ok(response);
  }
  
  [HttpPost]
  public async Task<ActionResult> CreateBook([FromBody] BooksRequest booksRequest)
  {
    var bookGenre = await _genreRepository.GetGenreByIdAsync(booksRequest.GenreId);

    if (bookGenre is null)
      return NotFound($"genre with id : {booksRequest.GenreId} was not found ! ");
    
    Books book = null;
    try
    {
      book = await Books.CreateAsync(
        _booksRepository,
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

    await _booksRepository.CreateBookAsync(book);
    await _booksRepository.SaveChangesAsync();
    
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
    var book = await _booksRepository.GetBookByIdAsync(Id);
  
    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

   await _booksRepository.RemoveBookAsync(Id);
   
  
    return Ok($"Book with Id : {Id} was removed ! ");
  }
  
  [HttpPatch("{Id}")]
  public async Task<ActionResult> ChangeName(string Id, [FromBody] string name)
  {
    var book = await _booksRepository.GetBookByIdAsync(Id);
    
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

    await _booksRepository.UpdateBookAsync(book);
    
    return Ok(book);
  }
  
  [HttpPut("{Id}")]
  public async Task<ActionResult> ChangeBook(string Id, [FromBody] BooksRequest booksRequest)
  {
    var bookTask = _booksRepository.GetBookByIdAsync(Id);
    
    if (bookTask is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    var book = await bookTask;
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

    await _booksRepository.UpdateBookAsync(book);
    
    return Ok(book);
  }
  
}