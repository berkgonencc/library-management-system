using System;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data
{
	public class LibraryManagementDbContext : DbContext
	{
		public LibraryManagementDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
	}
}

