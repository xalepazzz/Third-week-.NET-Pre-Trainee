using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using thirdweek.Models;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        IAuthorRepository _repository;
        IBookRepository _bookRepository;
        public AuthorController(IAuthorRepository repository, IBookRepository bookRepository) { _repository = repository; _bookRepository = bookRepository; }

        [HttpGet("author/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            Author author = _repository.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            List<Author> authors = _repository.GetAllAuthors();
            if (authors.Count > 0)
            {
                return Ok(authors);
            }
            else { return NoContent(); }

        }

        [HttpPost]
        public IActionResult AddAuthor(string name, DateOnly? dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name) || !dateOfBirth.HasValue)
                return BadRequest("Необходимо передать name и dateOfBirth");

            bool result = _repository.AddAuthor(name, dateOfBirth.Value);
            if (!result) return BadRequest("Автор не был добавлен");
            return Ok(true);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, string? name, DateOnly? dateOfBirth)
        {
            var author = _repository.GetAuthorById(id);
            if (author == null) return NotFound();

            bool result = _repository.ModifyAuthor(id, name, dateOfBirth);
            if (!result) return BadRequest();
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _repository.GetAuthorById(id);
            if (author == null) return NotFound();

            var books = _bookRepository.GetAllBooks().Where(b => b.AuthorId == id).ToList();
            foreach (var book in books)
            {
                _bookRepository.DeleteBook(book.Id);
            }

            bool result = _repository.DeleteAuthor(id);
            if (!result) return BadRequest();
            return Ok(true);
        }
    }
}
