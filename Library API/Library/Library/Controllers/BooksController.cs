using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;

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
  public ActionResult GetBooks()
  {
    var books = _dbContext.Set<Books>().ToList();
    
    return Ok(books);
  }

  [HttpGet("{Id}")]
  public ActionResult GetBooks(string Id)
  {
    var book = _dbContext.BooksEnumerable.Where(booksi => booksi.Id == Id).OrderBy(booksi => booksi.Name)
      .FirstOrDefault();

    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    return Ok(book);
  }

  [HttpPost]
  public ActionResult CreateBook([FromBody] BooksRequest booksRequest)
  {
    Books book = null;

    try
    {
      book = Books.Create(booksRequest.Name, booksRequest.Author, booksRequest.Pages);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

    _dbContext.Add(book);
    _dbContext.SaveChanges();
    
    return Ok(book);
  }

  [HttpDelete("{Id}")]
  public ActionResult DeleteBook(string Id)
  {
    var book = _dbContext.BooksEnumerable.FirstOrDefault(b => b.Id == b.Id);

    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    _dbContext.Remove(book);
    _dbContext.SaveChanges();

    return Ok($"Book with Id : {Id} was removed ! ");
  }

  [HttpPatch("{Id}")]
  public ActionResult ChangeName(string Id, [FromBody] string name)
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

    _dbContext.SaveChanges();
    
    return Ok(book);
  }

  [HttpPut("{Id}")]
  public ActionResult ChangeBook(string Id, [FromBody] BooksRequest booksRequest)
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

    _dbContext.SaveChanges();
    
    return Ok(book);
  }
  
}