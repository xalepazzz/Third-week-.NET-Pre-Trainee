using thirdweek.Models;

namespace thirdweek
{
    public interface IBookRepository
    {
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        bool AddBook(string title,  DateOnly publishDate, int authorId);
        bool ModifyBook(int id, string? title, DateOnly? publishDate, int? authorId);
        bool DeleteBook(int id);
    }
}
