using DatabaseLayer.Models;

namespace DatabaseLayer.Interfaces
{
    public interface IBookRepository
    {
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        void AddBook(Book book);
        void ModifyBook(Book book);
        void DeleteBook(int id);
    }
}
