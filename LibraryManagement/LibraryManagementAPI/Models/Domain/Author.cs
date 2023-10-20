using System;
namespace LibraryManagementAPI.Models.Domain
{
	public class Author
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}

