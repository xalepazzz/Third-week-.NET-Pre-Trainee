using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using BuisnessLogic.Interfaces;
using DatabaseLayer.Models;
using DatabaseLayer;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        IBookService _bookService;
        IAuthorService _authorService;
        public BookController(IBookService bookService, IAuthorService authorService) { _bookService = bookService; _authorService = authorService; }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                Book book = _bookService.GetBookById(id);
                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<Book> books = _bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddBook(string title, DateOnly? publishDate, int? authorId)
        {
            try
            {
                if (!publishDate.HasValue || !authorId.HasValue)
                    return BadRequest("Необходимо указать publishDate и authorId");

                _bookService.AddBook(title, publishDate.Value, authorId.Value);
                return Ok("Книга успешно добавлена");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, string? title, DateOnly? publishDate, int? authorId)
        {
            try
            {
                if (authorId.HasValue)
                {
                    _authorService.GetAuthorById(authorId.Value);
                }

                _bookService.ModifyBook(id, title, publishDate, authorId);
                return Ok("Книга успешно обновлена");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return Ok("Книга успешно удалена");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}