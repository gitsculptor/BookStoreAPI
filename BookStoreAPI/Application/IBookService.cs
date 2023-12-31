using BookStoreAPI.Domain;

namespace BookStoreAPI.Application;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(Guid id);
    Task<Guid> CreateBookAsync(string title, string author, decimal price);
    Task UpdateBookAsync(Guid id, string title, string author, decimal price);
    Task DeleteBookAsync(Guid id);
}