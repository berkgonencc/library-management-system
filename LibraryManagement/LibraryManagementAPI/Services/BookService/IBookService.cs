using System;
using LibraryManagementAPI.Models.Domain;

namespace LibraryManagementAPI.Services.BookService
{
	public interface IBookService
	{
        Task<Book> CreateAsync(Book book);
    }
}

