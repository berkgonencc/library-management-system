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

        public async Task<List<Author>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();

        }
    }
}

