using DatabaseLayer.Models;

namespace DatabaseLayer.Interfaces
{
    public interface IAuthorRepository
    {
        Author GetAuthorById(int id);
        List<Author> GetAllAuthors();
        void AddAuthor(Author author);
        void ModifyAuthor(Author author);
        bool DeleteAuthor(int id);
    }
}
