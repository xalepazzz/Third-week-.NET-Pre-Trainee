namespace BuisnessLogic.Interfaces;
using DatabaseLayer.Models;

public interface IBookService
{
    Book GetBookById(int id);

    List<Book> GetAllBooks();

    void AddBook(string title, DateOnly publishDate, int authorId);

    void ModifyBook(int id, string? title, DateOnly? publishDate, int? authorId);

    void DeleteBook(int id);

}