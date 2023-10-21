using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Helpers;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("booksByAuthor")]
        public IActionResult GetBooksByAuthor(string authorName)
        {
            try
            {
                var library = _libraryService.GetLibrary();
                var books = library.GetBooksByAuthor(authorName);
                return Ok(books);
            }
            catch (Exception ex)
            {
                //TODO:logger
                return BadRequest(new ResponseMessage { Message = ex.Message });
            }

        }

        [HttpGet("checkedOutBooks")]
        public IActionResult GetAllCheckedOutBooks()
        {
            try
            {
                var library = _libraryService.GetLibrary();
                var books = library.GetAllCheckedOutBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                //TODO:logger
                return BadRequest(new ResponseMessage { Message = ex.Message });

            }

        }

        [HttpPost("checkout/{isbn}")]
        public async Task<ActionResult<bool>> CheckOutBookAsync(string isbn)
        {
            try
            {
                var library = _libraryService.GetLibrary();
                var result = await library.CheckOutBookAsync(isbn);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO:logger
                return BadRequest(new ResponseMessage { Message = ex.Message });

            }

        }
    }
}

