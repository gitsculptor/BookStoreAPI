using BookStoreAPI.Application;
using BookStoreAPI.Domain;
using BookStoreAPI.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


// Build the configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Add the configuration to services
builder.Services.AddSingleton(configuration);

// Add MongoDB configuration from appsettings.json
var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
builder.Services.AddSingleton(mongoDbSettings);

// Register the DbContext
builder.Services.AddSingleton<MongoDbContext>();

// Register the Repository
builder.Services.AddScoped<IBookRepository, MongoBookRepository>();

// Register the Service
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();