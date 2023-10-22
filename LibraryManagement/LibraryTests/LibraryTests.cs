using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryTests;

public class LibraryTests
{
    [Fact]
    public async Task GetBooksByAuthor_Should_ReturnBooksByAuthor()
    {
        // Arrange
        var authorName = "Berk Gonenc";
        var options = new DbContextOptionsBuilder<LibraryManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using (var context = new LibraryManagementDbContext(options))
        {
            var book1 = new Book { Author = new Author { Name = authorName }, Title = "Book 1", ISBN = "ISBN-1" };
            var book2 = new Book { Author = new Author { Name = authorName }, Title = "Book 2", ISBN = "ISBN-2" };
            var book3 = new Book { Author = new Author { Name = "Jane Smith" }, Title = "Book 3", ISBN = "ISBN-3" };

            context.Books.AddRange(book1, book2, book3);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryManagementDbContext(options))
        {
            var libraryService = new LibraryService(context);

            // Act
            var booksByAuthor = libraryService.GetBooksByAuthor(authorName);

            // Assert
            Assert.Equal(2, booksByAuthor.Count);
        }
    }


    [Fact]
    public async Task GetAllCheckedOutBooks_ReturnsListOfCheckedOutBooks()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LibraryManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using (var context = new LibraryManagementDbContext(options))
        {
            context.Books.AddRange(
                new Book { Title = "Book1", ISBN = "ISBN-1", Author = new Author { Name = "AuthorName" }, CheckedOut = true },
                new Book { Title = "Book2", ISBN = "ISBN-2", Author = new Author { Name = "AuthorName" }, CheckedOut = false },
                new Book { Title = "Book3", ISBN = "ISBN-3", Author = new Author { Name = "AuthorName" }, CheckedOut = true },
                new Book { Title = "Book4", ISBN = "ISBN-4", Author = new Author { Name = "OtherAuthor" }, CheckedOut = true }
            );
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryManagementDbContext(options))
        {
            var libraryService = new LibraryService(context);

            // Act
            var checkedOutBooksResult = libraryService.GetAllCheckedOutBooks();

            // Assert
            Assert.Equal(3, checkedOutBooksResult.Count);
        }
    }

    [Fact]
    public async Task CheckOutBookAsync_ValidISBN_ShouldCheckOutBook()
    {
        // Arrange
        // sets up an in-memory database 
        var options = new DbContextOptionsBuilder<LibraryManagementDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

        //adds a book with a specific ISBN
        var book = new Book
        {
            ISBN = "123456789",
            Title = "Test Book",
            Author = new Author { Name = "John Doe" },
            CheckedOut = false
        };

        using (var context = new LibraryManagementDbContext(options))
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        using (var context = new LibraryManagementDbContext(options))
        {
            var libraryService = new LibraryService(context);

            // Act
            var result = await libraryService.CheckOutBookAsync("123456789");

            // Assert
            Assert.True(result, "Expected check-out operation to succeed.");

            var checkedOutBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123456789");
            Assert.NotNull(checkedOutBook);
            Assert.True(checkedOutBook.CheckedOut, "The book should be checked out.");
        }
    }

}
