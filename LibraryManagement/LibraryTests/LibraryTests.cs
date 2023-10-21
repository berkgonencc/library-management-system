using LibraryManagementAPI.Models.Domain;

namespace LibraryTests;

public class LibraryTests
{
    [Fact]
    public void GetBooksByAuthor_ShouldReturnBooksByAuthor()
    {
        // Arrange
        var library = new Library();
        var author = new Author { Name = "John Doe" };
        var book1 = new Book { Title = "Book 1", Author = author };
        var book2 = new Book { Title = "Book 2", Author = author };
        var book3 = new Book { Title = "Book 3", Author = new Author { Name = "Jane Smith" } };
        library.Books.AddRange(new List<Book> { book1, book2, book3 });

        // Act
        var booksByAuthor = library.GetBooksByAuthor("John Doe");

        // Assert
        Assert.Equal(2, booksByAuthor.Count);
    }
    [Fact]
    public void GetAllCheckedOutBooks_ReturnsListOfCheckedOutBooks()
    {
        //Arrange
        var library = new Library();
        library.Books = new List<Book>
         {
            new Book { Title = "Book1", ISBN = "ISBN-1", Author = new Author { Name = "AuthorName" }, CheckedOut = true },
            new Book { Title = "Book2", ISBN = "ISBN-2", Author = new Author { Name = "AuthorName" }, CheckedOut = false },
            new Book { Title = "Book3", ISBN = "ISBN-3", Author = new Author { Name = "AuthorName" }, CheckedOut = true },
            new Book { Title = "Book4", ISBN = "ISBN-4", Author = new Author { Name = "OtherAuthor" }, CheckedOut = true },
        };

        //Act
        var checkedOutBooks = library.GetAllCheckedOutBooks();
        //Assert
        Assert.Equal(3, checkedOutBooks.Count);

    }

    [Fact]
    public async Task CheckOutBookAsync_ShouldCheckOutBook()
    {
        // Arrange
        var library = new Library();
        var book = new Book { Title = "Book 1", ISBN = "1234" };
        library.Books.Add(book);

        // Act
        var result = await library.CheckOutBookAsync("1234");

        // Assert
        Assert.True(result);
        Assert.True(book.CheckedOut);
    }
}
