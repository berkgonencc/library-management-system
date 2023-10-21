using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;
using LibraryManagementAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(LibraryManagementDbContext dbContext,
IAuthorRepository authorRepository,
IMapper mapper)
        {
            _dbContext = dbContext;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from database - domain models
            var authorsDomain = await _authorRepository.GetAllAsync();

            //Map domain models to DTOs
            var authorsDto = _mapper.Map<List<AuthorDto>>(authorsDomain);
            //return DTOs
            return Ok(authorsDto);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var authorDomain = await _authorRepository.GetByIdAsync(id);
            if (authorDomain == null)
            {
                return NotFound();
            }
            // Return dto back to client
            return Ok(_mapper.Map<AuthorDto>(authorDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var authorDomainModel = _mapper.Map<Author>(addAuthorRequestDto);
                //Use Domain Model to create Author
                authorDomainModel = await _authorRepository.CreateAsync(authorDomainModel);

                //Map domain model back to DTO
                var authorDto = _mapper.Map<Author>(authorDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = authorDto.Id }, authorDto);
            } else
            {
                return BadRequest(ModelState);
            }
          
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var authorDomainModel = _mapper.Map<Author>(updateAuthorRequestDto);
                //Check if author exists
                authorDomainModel = await _authorRepository.UpdateAsync(id, authorDomainModel);
                if (authorDomainModel == null) return NotFound();

                //return Domain Model to DTO
                return Ok(_mapper.Map<AuthorDto>(authorDomainModel));
            } else
            {
                return BadRequest(ModelState);
            }
       
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var authorDomainModel = await _authorRepository.DeleteAsync(id);
            if (authorDomainModel == null)
            {
                return NotFound();
            }

            //map Domain Model to DTO
            var authorDto = _mapper.Map<AuthorDto>(authorDomainModel);
            return Ok(authorDto);
        }
    }
}

