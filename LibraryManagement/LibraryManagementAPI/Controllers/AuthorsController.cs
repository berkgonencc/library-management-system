using AutoMapper;
using LibraryManagementAPI.CustomActionFilters;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;
using LibraryManagementAPI.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(
IAuthorService authorService,
IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        //Get all Authors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from database - domain models
            var authorsDomain = await _authorService.GetAllAsync();

            //Map domain models to DTOs
            return Ok(_mapper.Map<List<AuthorDto>>(authorsDomain));

        }

        //Add a Author
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            //Map DTO to Domain Model
            var authorDomainModel = _mapper.Map<Author>(addAuthorRequestDto);
            //Use Domain Model to create Author
            authorDomainModel = await _authorService.CreateAsync(authorDomainModel);

            //Map domain model back to DTO
            return Ok(_mapper.Map<AuthorDto>(authorDomainModel));
        }

        // Delete Author
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var deletedAuthor = await _authorService.DeleteAsync(id);
            if (deletedAuthor == null) return NotFound();
            return Ok(_mapper.Map<AuthorDto>(deletedAuthor));

        }

    }
}

