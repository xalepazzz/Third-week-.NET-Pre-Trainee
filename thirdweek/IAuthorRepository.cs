using Microsoft.AspNetCore.Http.Metadata;
using thirdweek.Models;

namespace thirdweek
{
    public interface IAuthorRepository
    {
        Author GetAuthorById(int id);
        List<Author> GetAllAuthors();
        bool AddAuthor(string name, DateOnly DateOfBirth);
        bool ModifyAuthor(int id, string? name, DateOnly? dateOfBirth);
        bool DeleteAuthor(int id);
    }
}
