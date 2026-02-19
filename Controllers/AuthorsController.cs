using AutoMapper;
using BookStorePerfApi.DTOs.Authors;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStorePerfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorCommandRepository _commandRepo;
        private readonly IAuthorQueryRepository _queryRepo;
        private readonly IMapper _mapper;

        public AuthorsController(
            IAuthorCommandRepository commandRepo,
            IAuthorQueryRepository queryRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _queryRepo.GetAllAsync();
            var authordata = _mapper.Map<IEnumerable<Author>>(authors);
            return Ok(authordata);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _queryRepo.GetByIdAsync(id);
            if (author == null) return NotFound();
            var authordata = _mapper.Map<AuthorResponseDto>(author);
            return Ok(authordata);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var author = _mapper.Map<Author>(authorDto);
            var newId = await _commandRepo.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = newId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto authorDto)
        {
            if (id != authorDto.Id) return BadRequest("ID mismatch");
            var author = _mapper.Map<Author>(authorDto);
            try
            {
                await _commandRepo.UpdateAuthorAsync(id, author);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commandRepo.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
