using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Domain;
using LibraryManagementAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryManagementDbContext _dbContext;

        public AuthorController(LibraryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //get data from database - domain models
            var authors = await _dbContext.Authors.ToListAsync();

            //Map domain models to DTOs
            var authorsDto = new List<AuthorDto>();
            foreach (var author in authors)
            {
                authorsDto.Add(new AuthorDto { Id = author.Id, Name = author.Name });
            };
            //return DTOs
            return Ok(authorsDto);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            //Map Author Domain Model to Author DTO
            var authorDto = new AuthorDto { Id = author.Id, Name = author.Name };

            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            //Map DTO to Domain Model
            var authorDomainModel = new Author
            {
                Name = addAuthorRequestDto.Name,
            };
            //Use Domain Model to create Author
            await _dbContext.Authors.AddAsync(authorDomainModel);
            await _dbContext.SaveChangesAsync();

            //Map domain model back to DTO
            var authorDto = new AuthorDto { Id = authorDomainModel.Id, Name = authorDomainModel.Name };

            return CreatedAtAction(nameof(GetById), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            //Check if author exists
            var authorDomainModel = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (authorDomainModel == null) return NotFound();

            //Map DTO to Domain Model
            authorDomainModel.Name = updateAuthorRequestDto.Name;
            await _dbContext.SaveChangesAsync();

            //Convert Domain Model to DTO
            var authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name
            };
            return Ok(authorDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var authorDomainModel = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (authorDomainModel == null)
            {
                return NotFound();
            }

            _dbContext.Authors.Remove(authorDomainModel);
            await _dbContext.SaveChangesAsync();

            //map Domain Model to DTO
            var authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name
            };
            return Ok(authorDto);
        }
    }
}

