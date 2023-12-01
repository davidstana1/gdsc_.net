using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]

public class BooksController : ControllerBase
{
  private static readonly List<Books> books_list = new()
  {
    Books.Create("Atomic Habits", "James Clear", 256),
    Books.Create("Deep Work", "Cal Newport", 210)
  };

  [HttpGet(Name = "GetAllBooks")]
  public ActionResult GetBooks()
  {
    return Ok(books_list);
  }

  [HttpGet("{Id}")]
  public ActionResult GetBooks(string Id)
  {
    var book = books_list.FirstOrDefault(b => b.Id == Id);

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
    books_list.Add(book);
    return Ok(booksRequest);
  }

  [HttpDelete("{Id}")]
  public ActionResult DeleteBook(string Id)
  {
    var book = books_list.FirstOrDefault(b => b.Id == Id);

    if (book is null)
      return NotFound($"Book with ID : {Id} does not exist ! ");

    books_list.Remove(book);

    return Ok($"Book with Id : {Id} was removed ! ");
  }

  [HttpPatch("{Id}")]
  public ActionResult ChangeName(string Id, [FromBody] string name)
  {
    var book = books_list.FirstOrDefault(b => b.Id == Id);
    
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

    return Ok(book);
  }

  [HttpPut("{Id}")]
  public ActionResult ChangeBook(string Id, [FromBody] BooksRequest booksRequest)
  {
    var book = books_list.FirstOrDefault(b => b.Id == Id);
    
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

    return Ok(book);
  }
  
}