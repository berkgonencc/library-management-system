using System;
using LibraryManagementAPI.Models.Domain;

namespace LibraryManagementAPI.Services.AuthorService
{
	public interface IAuthorService
	{
        Task<List<Author>> GetAllAsync();
        Task<Author> CreateAsync(Author author);
    }
}

