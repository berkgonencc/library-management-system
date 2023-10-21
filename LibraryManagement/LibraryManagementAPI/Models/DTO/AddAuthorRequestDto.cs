using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagementAPI.Models.Domain;

namespace LibraryManagementAPI.Models.DTO
{
	public class AddAuthorRequestDto
	{
        [Required]
        [MinLength(5, ErrorMessage = "Name has to be at least 5 characters.")]
        public string Name { get; set; }
    }
}

