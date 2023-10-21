using System;
using AutoMapper;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;

namespace LibraryManagementAPI.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AddAuthorRequestDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorRequestDto, Author>().ReverseMap();

			CreateMap<AddBookRequestDto, Book>().ReverseMap();
			CreateMap<Book, BookDto>().ReverseMap();
			CreateMap<UpdateBookRequestDto, Book>().ReverseMap();

        }
    }
}

