using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models.DTO
{
	public class AddBookRequestDto
	{
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(0,20)]
        public string ISBN { get; set; }
        [Required]
        public bool CheckedOut { get; set; }
        [Required]
        //navigation props
        public Guid AuthorId { get; set; }
    }
}

