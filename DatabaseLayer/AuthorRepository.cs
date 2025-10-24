using System.Data.SqlTypes;
using DatabaseLayer.Models;

namespace DatabaseLayer
{
    public class AuthorRepository : IAuthorRepository
    {
        private static List<Author> _storage;
        public AuthorRepository() 
        {
            _storage = new List<Author>();
        }
        
        public Author GetAuthorById(int id) 
        {
            return _storage.Find(x => x.Id == id);
        }
        public List<Author> GetAllAuthors() 
        { 
            return _storage;
        }
        public void AddAuthor(Author author) 
        {
                author.Id = _storage.Any() ? _storage.Max(i => i.Id) + 1 : 1;
                _storage.Add(author);
        }
        public void ModifyAuthor( Author author) 
        {
            Author _author = GetAuthorById(author.Id);
            _author.Name = author.Name;
            _author.DateOfBirth = author.DateOfBirth;
        }
        public bool DeleteAuthor(int id) 
        {
            return _storage.RemoveAll(a => a.Id == id) > 0;
        }
    }
}
