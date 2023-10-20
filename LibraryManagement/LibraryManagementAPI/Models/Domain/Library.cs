using System;
namespace LibraryManagementAPI.Models.Domain
{
	public class Library
	{
		public ICollection<Book> Books { get; set; }
	}
}

