using DatabaseLayer;
using DatabaseLayer.Models;

namespace BuisnessLogic;

public class AuthorService(IAuthorRepository repository)
{
    public Author GetAuthorById(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID автора не может быть пустым");

        var author = repository.GetAuthorById(id);
        if (author == null)
            throw new ArgumentException($"Автор с ID {id} не найден");
        return author;
    }

    public List<Author> GetAllAuthors()
    {
        return repository.GetAllAuthors();
    }

    public void AddAuthor(string name, DateOnly dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя автора не может быть пустым");
        
        if (dateOfBirth == default(DateOnly))
            throw new ArgumentException("Дата рождения должна быть указана");

        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Дата рождения не может быть в будущем");

        var author = new Author()
        {
            Name = name.Trim(),
            DateOfBirth = dateOfBirth
        };
        
        repository.AddAuthor(author);
    }

    public void ModifyAuthor(int id, string? name, DateOnly? dateOfBirth)
    {
        if (id == 0)
            throw new ArgumentException("ID автора не может быть пустым");

        var existingAuthor = GetAuthorById(id);
        
        string updatedName = existingAuthor.Name;
        DateOnly updatedDateOfBirth = existingAuthor.DateOfBirth;

        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя автора не может быть пустым");
            updatedName = name.Trim();
        }
        
        if (dateOfBirth.HasValue)
        {
            if (dateOfBirth.Value == default(DateOnly))
                throw new ArgumentException("Дата рождения должна быть указана");
                
            if (dateOfBirth.Value > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата рождения не может быть в будущем");
            updatedDateOfBirth = dateOfBirth.Value;
        }

        var updatedAuthor = new Author()
        {
            Id = id,
            Name = updatedName,
            DateOfBirth = updatedDateOfBirth
        };
        
        repository.ModifyAuthor(updatedAuthor);
    }

    public void DeleteAuthor(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID автора должен быть пустым");

        GetAuthorById(id);
        repository.DeleteAuthor(id);
    }
}