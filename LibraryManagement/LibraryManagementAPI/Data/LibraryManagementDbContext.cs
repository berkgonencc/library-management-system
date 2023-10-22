using System;
using LibraryManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var authors = new List<Author>()
            {
                new Author()
                        {
                            Id = Guid.Parse("d0be27e1-4515-4f81-a919-b1204ef58b10"),
                            Name = "Ray Bradbury"
                        },
                new Author()
                        {
                            Id = Guid.Parse("03ed0716-89d8-4267-9fa1-44271ffd3339"),
                            Name = "Aldous Huxley"
                        },
                new Author()
                        {
                            Id = Guid.Parse("87a25f21-2190-4c16-bb39-86fbd6da0de6"),
                            Name = "Mary Shelley"
                        }
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var books = new List<Book>()
            {
                new Book
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    Title = "Fahrenheit 451",
                    ISBN = "0451524934",
                    CheckedOut = false,
                    AuthorId = Guid.Parse("d0be27e1-4515-4f81-a919-b1204ef58b10")
                },
                new Book
                {
                    Id = Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                    Title = "The Halloween Tree",
                    ISBN = "0735619670",
                    CheckedOut = false,
                    AuthorId = Guid.Parse("d0be27e1-4515-4f81-a919-b1204ef58b10")
                },
                new Book
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                    Title = "Brave New World",
                    ISBN = "0735619444",
                    CheckedOut = true,
                    AuthorId = Guid.Parse("03ed0716-89d8-4267-9fa1-44271ffd3339")
                },
                new Book
                {
                    Id = Guid.Parse("0e344965-9852-4c8b-a0b0-4f1e2c8fd8e3"),
                    Title = "The Art of Seeing",
                    ISBN = "0731234570",
                    CheckedOut = false,
                    AuthorId = Guid.Parse("03ed0716-89d8-4267-9fa1-44271ffd3339")
                },
                new Book
                {
                    Id = Guid.Parse("1eb2c51f-4b19-4a95-9bbf-90cb6a23d218"),
                    Title = "Frankenstein",
                    ISBN = "0735619688",
                    CheckedOut = false,
                    AuthorId = Guid.Parse("87a25f21-2190-4c16-bb39-86fbd6da0de6")
                },
            };
            modelBuilder.Entity<Book>().HasData(books);

        }
    }
}

