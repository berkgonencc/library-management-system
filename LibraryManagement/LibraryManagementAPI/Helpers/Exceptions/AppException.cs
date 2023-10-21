using System;
namespace LibraryManagementAPI.Helpers.Exceptions
{
	public class AppException : Exception
	{
		public AppException() : base() { }

		public AppException(string message) : base(message) { }


	}
}

