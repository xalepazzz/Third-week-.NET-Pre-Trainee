namespace BuisnessLogic.Interfaces;

using DatabaseLayer.Models;

public interface IAuthorService
{
    Author GetAuthorById(int id);

    List<Author> GetAllAuthors();

    void AddAuthor(string name, DateOnly dateOfBirth);

    void ModifyAuthor(int id, string? name, DateOnly? dateOfBirth);

    void DeleteAuthor(int id);
}