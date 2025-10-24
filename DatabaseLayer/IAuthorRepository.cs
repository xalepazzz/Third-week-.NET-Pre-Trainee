using Microsoft.AspNetCore.Http.Metadata;
using DatabaseLayer.Models;

namespace DatabaseLayer
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
