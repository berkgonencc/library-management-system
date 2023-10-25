using System;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryManagementDbContext _dbContext;

        public AuthorService(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> DeleteAsync(Guid id)
        {
            var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (existingAuthor == null) return null;
            _dbContext.Authors.Remove(existingAuthor);
            await _dbContext.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();

        }
    }
}

