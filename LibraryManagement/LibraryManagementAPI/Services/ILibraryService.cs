using System;
using LibraryManagementAPI.Models.Domain;

namespace LibraryManagementAPI.Services
{
	public interface ILibraryService
	{
        Library GetLibrary();
    }
}

