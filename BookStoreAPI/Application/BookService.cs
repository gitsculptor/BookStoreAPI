using BookStoreAPI.Domain;

namespace BookStoreAPI.Application;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

    }
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllBooksAsync();
         
    }

    public  async Task<Book> GetBookByIdAsync(Guid id)
    {
        return await _bookRepository.GetBookByIdAsync(id);
    }

    public async Task<Guid> CreateBookAsync(string title, string author, decimal price)
    {
        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = title,
            Author = author,
            Price = price
        };

        await _bookRepository.CreateBookAsync(newBook);
        return newBook.Id;
    }

    public async Task UpdateBookAsync(Guid id, string title, string author, decimal price)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(id);

        if (existingBook == null)
        {
            throw new ArgumentException("Book not found");
        }

        existingBook.Title = title;
        existingBook.Author = author;
        existingBook.Price = price;

        await _bookRepository.UpdateBookAsync(existingBook);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _bookRepository.DeleteBookAsync(id);
    }
}