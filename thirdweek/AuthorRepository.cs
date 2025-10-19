using System.Data.SqlTypes;
using thirdweek.Models;

namespace thirdweek
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
        public bool AddAuthor(string name, DateOnly dateOfBirth) 
        {
            try
            {
                int storageLength = _storage.Count;
                _storage.Add(new Author {Id = _storage.Any() ? _storage.Max(i => i.Id) + 1 : 1, Name = name, DateOfBirth = (DateOnly)dateOfBirth });
                return _storage.Count > storageLength;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public bool ModifyAuthor(int id, string? name, DateOnly? dateOfBirth) 
        {
            Author author = GetAuthorById(id);
            if (author == null) return false;
            author.Name = string.IsNullOrEmpty(name) ? author.Name : name;
            author.DateOfBirth = dateOfBirth.HasValue ? (DateOnly)dateOfBirth : author.DateOfBirth;
            return true;
        }
        public bool DeleteAuthor(int id) 
        {
            return _storage.RemoveAll(a => a.Id == id) > 0;
        }
    }
}
