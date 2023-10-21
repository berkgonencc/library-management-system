using System;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryManagementDbContext _dbContext;

        public AuthorRepository(LibraryManagementDbContext dbContext)
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
            var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null) return null;
            _dbContext.Authors.Remove(existingAuthor);
            await _dbContext.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(Guid id, Author author)
        {
            var existingAuthor = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null)
            {
                return null;
            }
            existingAuthor.Name = author.Name;
            await _dbContext.SaveChangesAsync();
            return existingAuthor;
        }
    }
}

