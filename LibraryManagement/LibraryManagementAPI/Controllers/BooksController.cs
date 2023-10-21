using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.CustomActionFilters;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;
using LibraryManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BooksController(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        // api/books?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000
            )
        {
            var booksDomainModel = await _bookRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //map to dto
            return Ok(_mapper.Map<List<BookDto>>(booksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bookDomainModel = await _bookRepository.GetByIdAsync(id);
            if (bookDomainModel == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(bookDomainModel));

        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddBookRequestDto addBookRequestDto)
        {
            //Map DTO to Domain Model
            var bookDomainModel = _mapper.Map<Book>(addBookRequestDto);

            await _bookRepository.CreateAsync(bookDomainModel);

            //return Domain Model to DTO
            return Ok(_mapper.Map<BookDto>(bookDomainModel));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateBookRequestDto updateBookRequestDto)
        {

            var bookDomainModel = _mapper.Map<Book>(updateBookRequestDto);

            bookDomainModel = await _bookRepository.UpdateAsync(id, bookDomainModel);
            if (bookDomainModel == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(bookDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedBookDomainModel = await _bookRepository.DeleteAsync(id);
            if (deletedBookDomainModel == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(deletedBookDomainModel));
        }
    }
}

