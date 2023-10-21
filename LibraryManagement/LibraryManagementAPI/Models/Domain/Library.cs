using System;
using System.Text.Json.Serialization;
using LibraryManagementAPI.Models.DTO;

namespace LibraryManagementAPI.Models.Domain
{
    public class Library
    {
        public List<Book> Books { get; set; }

        public List<BookDto> GetBooksByAuthor(string authorName)
        {
            return Books.Where(b => b.Author.Name == authorName).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                CheckedOut = b.CheckedOut,
                Author = new AuthorDto
                {
                    Id = b.AuthorId,
                    Name = b.Author.Name
                }
            }).ToList();
        }

        public List<BookDto> GetAllCheckedOutBooks()
        {
            return Books.Where(b => b.CheckedOut).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                CheckedOut = b.CheckedOut,
                Author = new AuthorDto
                {
                    Id = b.AuthorId,
                    Name = b.Author.Name
                }
            }).ToList();
        }

        public async Task<bool> CheckOutBookAsync(string isbn)
        {
            Book book = Books.FirstOrDefault(b => b.ISBN == isbn);

            if (book != null && !book.CheckedOut)
            {
                // Simulate long-running operation
                await Task.Delay(2000);

                book.CheckedOut = true;
                return true;
            }

            return false;
        }

    }

}

