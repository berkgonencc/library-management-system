using System;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementDbContext _dbContext;

        public BookRepository(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(Guid id)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null) return null;
            _dbContext.Books.Remove(existingBook);
            await _dbContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<List<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var books = _dbContext.Books.Include("Author").AsQueryable();
            //Filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = books.Where(x => x.Title.Contains(filterQuery));

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
            var skipResults = (pageNumber - 1) * pageSize;

            return await books.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await _dbContext.Books.Include("Author").ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Books.Include("Author").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateAsync(Guid id, Book book)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.CheckedOut = book.CheckedOut;
            existingBook.AuthorId = book.AuthorId;
            await _dbContext.SaveChangesAsync();

            return existingBook;
        }
    }
}

