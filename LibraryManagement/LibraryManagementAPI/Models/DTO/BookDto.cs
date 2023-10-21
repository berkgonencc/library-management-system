using System;
using System.Text.Json.Serialization;

namespace LibraryManagementAPI.Models.DTO
{
	public class BookDto
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool CheckedOut { get; set; }
        public AuthorDto Author { get; set; }
    }
}

