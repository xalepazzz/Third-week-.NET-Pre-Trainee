using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using BuisnessLogic;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;
        
        public AuthorController(AuthorService authorService, BookService bookService) 
        { 
            _authorService = authorService; 
            _bookService = bookService; 
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            try
            {
                Author author = _authorService.GetAuthorById(id);
                return Ok(author);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            try
            {
                List<Author> authors = _authorService.GetAllAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, DateOnly? dateOfBirth)
        {
            try
            {
                if (!dateOfBirth.HasValue)
                    return BadRequest("Необходимо указать dateOfBirth");

                _authorService.AddAuthor(name, dateOfBirth.Value);
                return Ok("Автор успешно добавлен");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, string? name, DateOnly? dateOfBirth)
        {
            try
            {
                _authorService.ModifyAuthor(id, name, dateOfBirth);
                return Ok("Автор успешно обновлен");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                var books = _bookService.GetAllBooks().Where(b => b.AuthorId == id).ToList();
                foreach (var book in books)
                {
                    _bookService.DeleteBook(book.Id);
                }

                _authorService.DeleteAuthor(id);
                return Ok("Автор и связанные книги успешно удалены");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}