using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using BookStoreAPI.Domain;

namespace BookStoreAPI.Infrastructure;

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(MongoDbSettings mongoDbSettings)
        {
           
            var client = new MongoClient(mongoDbSettings.MongoDB);
            _database = client.GetDatabase(mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    }
