using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models.DTO
{
	public class UpdateAuthorRequestDto
	{
        [Required]
        [MinLength(5, ErrorMessage = "Name has to be at least 5 characters.")]
        public string Name { get; set; }

    }
}

