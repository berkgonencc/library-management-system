using System;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryManagementDbContext _dbContext;

        public BookService(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(book).Reference(b => b.Author).LoadAsync();
            return book;
        }    
    }
}

