using LibraryManagementAPI.Models;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;

namespace LibraryManagementAPI.Services
{
    public interface ILibraryService
    {
        Task<List<Book>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
        List<Book> GetBooksByAuthor(string authorName);
        List<Book> GetAllCheckedOutBooks();
        Task<bool> CheckOutBookAsync(string isbn);
    };
}

