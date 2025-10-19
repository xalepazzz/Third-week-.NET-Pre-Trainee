using thirdweek.Models;

namespace thirdweek
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
        public bool AddBook(string title, DateOnly publishDate, int authorId)
        {
            try
            {
                int storageLength = _storage.Count;
                _storage.Add(new Book { Id = _storage.Any() ? _storage.Max(i => i.Id) + 1 : 1, Title = title, PublishDate = (DateOnly)publishDate, AuthorId = authorId });
                return _storage.Count > storageLength;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool ModifyBook(int id, string? title, DateOnly? publishDate, int? authorId)
        {
            Book book = GetBookById(id);
            if (book == null) return false;
            book.Title = string.IsNullOrEmpty(title) ? book.Title : title;
            book.PublishDate = publishDate.HasValue ? (DateOnly)publishDate : book.PublishDate;
            book.AuthorId = authorId.HasValue ? (int)authorId : book.AuthorId;
            return true;
        }
        public bool DeleteBook(int id)
        {
            return _storage.RemoveAll(b => b.Id == id) > 0;
        }

    }
}
