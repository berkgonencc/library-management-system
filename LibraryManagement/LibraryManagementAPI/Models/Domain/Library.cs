using LibraryManagementAPI.Models.Domain;

namespace LibraryManagementAPI.Models
{
    public class Library
    {
        public ICollection<Book> Books { get; set; }
    }

}

