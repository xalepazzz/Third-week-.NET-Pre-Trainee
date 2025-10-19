using Microsoft.AspNetCore.Mvc;
using thirdweek.Models;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        IBookRepository _repository;
        IAuthorRepository _authorRepository;
        public BookController(IBookRepository repository, IAuthorRepository authorRepository) { _repository = repository; _authorRepository = authorRepository; }

        [HttpGet("Book/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            Book book = _repository.GetBookById(id);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<Book> book = _repository.GetAllBooks();
            if (book.Count > 0)
            {
                return Ok(book);
            }
            else { return NoContent(); }

        }

        [HttpPost]
        public IActionResult AddBook(string title, DateOnly? publishDate, int? authorId)
        {
            if (string.IsNullOrWhiteSpace(title) || !publishDate.HasValue || !authorId.HasValue)
                return BadRequest("Необходимо передать title, publishDate и authorId");

            var author = _authorRepository.GetAuthorById(authorId.Value);
            if (author == null)
                return NotFound("Автор с таким id не существует");

            bool result = _repository.AddBook(title, publishDate.Value, authorId.Value);
            if (!result)
                return BadRequest("Книга не была добавлена");

            return Ok(true);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, string? title, DateOnly? publishDate, int? authorId )
        {
            var existing = _repository.GetBookById(id);
            if (existing == null) return NotFound();
            if (authorId.HasValue)
            {
                var author = _authorRepository.GetAuthorById(authorId.Value);
                if (author == null)
                    return NotFound("Автор с таким id не существует");
            }

            bool result = _repository.ModifyBook(id, title, publishDate, authorId);
            if (!result) return BadRequest();
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var existing = _repository.GetBookById(id);
            if (existing == null) return NotFound();
            bool result = _repository.DeleteBook(id);
            if (!result) return BadRequest();
            return Ok(true);
        }
    }
}
