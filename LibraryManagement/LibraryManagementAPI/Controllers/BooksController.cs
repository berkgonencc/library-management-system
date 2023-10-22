using AutoMapper;
using LibraryManagementAPI.CustomActionFilters;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;
using LibraryManagementAPI.Services.BookService;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        // Create a new book
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddBookRequestDto addBookRequestDto)
        {
            //Map DTO to Domain Model
            var bookDomainModel = _mapper.Map<Book>(addBookRequestDto);
            //Use Domain Model to create Book
            await _bookService.CreateAsync(bookDomainModel);
            //return Domain Model to DTO
            return Ok(_mapper.Map<BookDto>(bookDomainModel));

        }    
    }
}

