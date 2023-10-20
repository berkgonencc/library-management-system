using System;
namespace LibraryManagementAPI.Models.Domain
{
	public class Book
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string ISBN { get; set; }
		public bool CheckedOut { get; set; }
		//navigation props
		public Guid AuthorId { get; set; }
		public Author Author { get; set; }
	}
}

