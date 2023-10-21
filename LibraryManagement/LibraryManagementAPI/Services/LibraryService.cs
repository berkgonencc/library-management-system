using System;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private Library library;
        private readonly LibraryManagementDbContext _context;

        public LibraryService(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public Library GetLibrary()
        {
            if (library == null)
            {
                library = new Library
                {
                    Books = _context.Books.Include(b => b.Author).ToList()
                };
            }
            return library;
        }
    }
}

