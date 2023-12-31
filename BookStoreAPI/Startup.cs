using Amazon.Lambda.Annotations;
using BookStoreAPI.Application;
using BookStoreAPI.Domain;
using BookStoreAPI.Infrastructure;

namespace BookStoreAPI;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Build the configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Add the configuration to services
        services.AddSingleton(configuration);

        // Add MongoDB configuration from appsettings.json
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        services.AddSingleton(mongoDbSettings);

        // Register the DbContext
        services.AddSingleton<MongoDbContext>();

        // Register the Repository
        services.AddScoped<IBookRepository, MongoBookRepository>();

        // Register the Service
        services.AddScoped<IBookService, BookService>();

        
    }
}