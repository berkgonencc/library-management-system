using AutoMapper;
using LibraryManagementAPI.Models.DTO;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly IMapper _mapper;

        public LibraryController(ILibraryService libraryService, IMapper mapper)
        {
            _libraryService = libraryService;
            _mapper = mapper;
        }

        // GET: api/library
        // Retrieve a list of books with optional filtering, and sorting.
        // api/books?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending
            )
        {
            // Retrieve books based on specified filters, and/or sorting
            var booksDomainModel = await _libraryService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            // Return domain model to dto
            return Ok(_mapper.Map<List<BookDto>>(booksDomainModel));
        }

        // GET: api/library/booksByAuthor
        // Retrieve a list of books by a specific author.
        [HttpGet("books-by-author")]
        public IActionResult GetBooksByAuthor([FromQuery] string authorName)
        {
            var books = _libraryService.GetBooksByAuthor(authorName);
            if (books.Any())
            {
                //return domain model to dto
                return Ok(_mapper.Map<List<BookDto>>(books));
            }
            return NotFound($"No books found for author: {authorName}");
        }

        // GET: api/library/checkedOutBooks
        // Retrieve a list of checked-out books.
        [HttpGet("checked-out-books")]
        public IActionResult GetAllCheckedOutBooks()
        {
            var books = _libraryService.GetAllCheckedOutBooks();
            //return domain model to dto
            return Ok(_mapper.Map<List<BookDto>>(books));
        }

        // PUT: api/library/checkout/{isbn}
        // Check out a book by ISBN and return a result.
        [HttpPut("checkout/{isbn}")]
        public async Task<ActionResult<bool>> CheckOutBookAsync(string isbn)
        {
            var result = await _libraryService.CheckOutBookAsync(isbn);
            return Ok(result);
        }
    }
}

