using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

namespace MinimalAPI.APIs
{
    public class BookApi
    {
        // Simulate the db
        private readonly DatabaseSimulator _db = new ();

        private const string CommandName = "Setters";
        private const string QueryName = "Getters";

        public void Configure(WebApplication? app)
        {
            if (app == null) return;

            ConfigureGetEndpoints(app);
            ConfigurePostEndpoints(app);
            ConfigurePutEndpoints(app);
        }

        private void ConfigureGetEndpoints(WebApplication app)
        {
            // Get all books
            app.MapGet("/books", async () => await _db.GetBooks())
                .Produces<List<Book>>(StatusCodes.Status200OK)
                .WithName("GetAllBooks")
                .WithTags(QueryName);

            // Get Book by Id
            app.MapGet("/books/{id}", async (int id) =>
            {
                var existingBook = await _db.GetBook(id);
                if (existingBook == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(existingBook);
            })
                .Produces<Book>(StatusCodes.Status200OK)
                .WithName("GetBookById")
                .WithTags(QueryName);

            // Get by keyword
            app.MapGet("/books/search/{query}", (string query) =>
            {
                var books = _db.QueryBooks(query);
                return books.Count > 0 ? Results.Ok(books) : Results.NotFound();
            })
                .Produces<List<Book>>(StatusCodes.Status200OK)
                .WithName("Search")
                .WithTags(QueryName);

            // Get paginated result set
            app.MapGet("books-by-page", async (int pageNumber, int pageSize) =>
            {
                var books = await _db.GetBooks();
                return books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            })
                .Produces<List<Book>>(StatusCodes.Status200OK)
                .WithName("GetBooksByPage")
                .WithTags(QueryName);
        }

        private void ConfigurePostEndpoints(WebApplication app)
        {
            // Create a new book
            app.MapPost("/books", async ([FromBody] Book newBook, HttpResponse response) =>
            {
                await _db.AddBook(newBook);

                response.StatusCode = 200;
                response.Headers.Location = $"books/{newBook.BookID}";
            })
                .Accepts<Book>("application/json")
                .Produces<Book>(StatusCodes.Status201Created)
                .WithName("AddNewBook")
                .WithTags(CommandName);
        }

        private void ConfigurePutEndpoints(WebApplication app)
        {
            // Update existing book using the ID
            app.MapPut("/books", [AllowAnonymous] async (int bookId, string bookTitle, HttpResponse response) =>
            {
                var existingBook = await _db.GetBook(bookId);

                if (existingBook == null)
                {
                    return Results.NotFound();
                }

                existingBook.Title = bookTitle;
                return Results.Created("/books", existingBook);
            })
                .Produces<Book>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("UpdateBook")
                .WithTags(CommandName);
        }
    }
}
