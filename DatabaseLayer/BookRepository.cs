using DatabaseLayer.Models;

namespace DatabaseLayer
{
    public class BookRepository : IBookRepository
    {
        private List<Book> _storage;
        public BookRepository()
        {
            _storage = new List<Book>();
        }

        public Book GetBookById(int id)
        {
            return _storage.Find(x => x.Id == id);
        }
        public List<Book> GetAllBooks()
        {
            return _storage;
        }
        public void AddBook(Book book)
        {
                book.Id = _storage.Any() ? _storage.Max(i => i.Id) + 1 : 1;
                _storage.Add(book);
        }
        public void ModifyBook(Book newBook)
        {
            Book book = GetBookById(newBook.Id);
            book.Title = newBook.Title;
            book.PublishDate = newBook.PublishDate;
            book.AuthorId = newBook.AuthorId;
        }
        public void DeleteBook(int id)
        {
            _storage.RemoveAll(b => b.Id == id);
        }

    }
}
