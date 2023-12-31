using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
// using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Core;
using BookStoreAPI.Application;



[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace BookStoreAPI.Controller;

public class BooksController 
{
    private const string RouteBase = "/books";
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get,RouteBase)]
    public async Task<IHttpResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return HttpResults.Ok(books);
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get,$"{RouteBase}/{{id}}")]
    public async Task<IHttpResult> GetBookByIdAsync(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book is null)
        {
            return HttpResults.NotFound();
        }

        return HttpResults.Ok(book);
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Post,RouteBase)]
    public async Task<IHttpResult> CreateBookAsync([FromBody] BookDto bookDto)
    {
        var newBookId = await _bookService.CreateBookAsync(bookDto.Title, bookDto.Author, bookDto.Price);
        return HttpResults.Created();
        

    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Put,$"{RouteBase}/{{id}}")]
    public async Task<IHttpResult> UpdateBookAsync(Guid id, [FromBody] BookDto bookDto)
    {
        try
        {
            await _bookService.UpdateBookAsync(id, bookDto.Title, bookDto.Author, bookDto.Price);
            return HttpResults.Ok();
        }
        catch (ArgumentException)
        {
            return HttpResults.NotFound();
        }
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Delete,$"{RouteBase}/{{id}}")]
    public async Task<IHttpResult>DeleteBookAsync(Guid id)
    {
        await _bookService.DeleteBookAsync(id);
        return HttpResults.Ok();
    }
}