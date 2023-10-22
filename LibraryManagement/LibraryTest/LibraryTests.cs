using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LibraryTest;

public class LibraryTests
{
    private LibraryService _libraryService;
    private Mock<LibraryManagementDbContext> _dbContextMock;

    [SetUp]
    public void Setup()
    {
        // Initialize the Mock DbContext
        _dbContextMock = new Mock<LibraryManagementDbContext>();

        // Create a list of sample books for testing
        var books = new List<Book>
            {
                new Book { ISBN = "123456", Title = "Book 1", Author = new Author { Name = "John Doe" }, CheckedOut = false },
                new Book { ISBN = "7891011", Title = "Book 2", Author = new Author { Name = "Jane Smith" }, CheckedOut = true },
                new Book { ISBN = "121314", Title = "Book 3", Author = new Author { Name = "John Doe" }, CheckedOut = false }
            };

        // Mock the DbSet and set it to return the sample books list
        var dbSetMock = new Mock<DbSet<Book>>();
        dbSetMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.AsQueryable().GetEnumerator());

        // Setup the mock DbContext to return the mock DbSet
        _dbContextMock.Setup(m => m.Books).Returns(dbSetMock.Object);

        // Create an instance of the LibraryService using the mock DbContext
        _libraryService = new LibraryService(_dbContextMock.Object);
    }

    [Test]
    public void GetBooksByAuthor_ValidAuthor_ReturnsBooksByAuthor()
    {
        // Arrange
        string authorName = "John Doe";
        int expectedCount = 2;

        // Act
        List<Book> result = _libraryService.GetBooksByAuthor(authorName);

        // Assert
        Assert.AreEqual(expectedCount, result.Count);
        Assert.IsTrue(result.All(b => b.Author.Name == authorName));
    }

    [Test]
    public void GetAllCheckedOutBooks_ReturnsCheckedOutBooks()
    {
        // Arrange
        int expectedCount = 1;

        // Act
        List<Book> result = _libraryService.GetAllCheckedOutBooks();

        // Assert
        Assert.AreEqual(expectedCount, result.Count);
        Assert.IsTrue(result.All(b => b.CheckedOut));
    }

    [Test]
    public async Task CheckOutBookAsync_ExistingBookNotCheckedOut_ReturnsTrue()
    {
        // Arrange
        string isbn = "123456";

        // Act
        bool result = await _libraryService.CheckOutBookAsync(isbn);

        // Assert
        Assert.IsTrue(result);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Test]
    public async Task CheckOutBookAsync_ExistingBookAlreadyCheckedOut_ReturnsFalse()
    {
        // Arrange
        string isbn = "7891011";

        // Act
        bool result = await _libraryService.CheckOutBookAsync(isbn);

        // Assert
        Assert.IsFalse(result);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task CheckOutBookAsync_NonExistingBook_ReturnsFalse()
    {
        // Arrange
        string isbn = "999999";

        // Act
        bool result = await _libraryService.CheckOutBookAsync(isbn);

        // Assert
        Assert.IsFalse(result);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
