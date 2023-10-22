using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryManagementDbContext _dbContext;

        public LibraryService(LibraryManagementDbContext context)
        {
            _dbContext = context;
        }

        public List<Book> GetBooksByAuthor(string authorName)
        {
            authorName = authorName.ToLower();
            // list of books written by specific author
            return _dbContext.Books
                .Include("Author")
                .Where(b => b.Author.Name.ToLower() == authorName)
                .ToList();
        }

        public List<Book> GetAllCheckedOutBooks()
        {
            // list of books that are currently borrowed
            return _dbContext.Books.Include("Author").Where(b => b.CheckedOut).ToList();
        }

        // Changing CheckedOut status of the books
        public async Task<bool> CheckOutBookAsync(string isbn)
        {
            Book? book = _dbContext.Books.Include("Author").FirstOrDefault(b => b.ISBN == isbn);

            //if the book exists or not already checkedOut
            if (book != null && !book.CheckedOut)
            {
                // Simulate long-running operation
                await Task.Delay(3000);

                book.CheckedOut = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            Console.WriteLine($"Book with ISBN {isbn} is either not found or already checked out.");
            return false;
        }

        // Get all books with filters
        public async Task<List<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var books = _dbContext.Books.Include("Author").AsQueryable();

            //Filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                filterQuery = filterQuery.ToLower();
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Title.ToLower().Contains(filterQuery));

                }
                else if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Author.Name.ToLower().Contains(filterQuery));

                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = isAscending ? books.OrderBy(x => x.Title) : books.OrderByDescending(x => x.Title);

                }
                else if (sortBy.Equals("ISBN", StringComparison.OrdinalIgnoreCase))
                {
                    books = isAscending ? books.OrderBy(x => x.ISBN) : books.OrderByDescending(x => x.ISBN);
                }
            }

            //Pagination
            //var skipResults = (pageNumber - 1) * pageSize;
            //var result = await books.Skip(skipResults).Take(pageSize).ToListAsync();
            return await books.ToListAsync();
        }
    }
}

