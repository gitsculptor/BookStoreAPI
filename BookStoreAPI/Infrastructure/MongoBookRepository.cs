using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using BookStoreAPI.Domain;

namespace BookStoreAPI.Infrastructure;

public class MongoBookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _books;

    public MongoBookRepository(MongoDbContext context)
    {
        _books = context.Books;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _books.Find(book => true).ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        return await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
    }`

    public async Task CreateBookAsync(Book book)
    {
        await _books.InsertOneAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        var updateResult = await _books.ReplaceOneAsync(b => b.Id == book.Id, book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _books.DeleteOneAsync(book => book.Id == id);
    }
}