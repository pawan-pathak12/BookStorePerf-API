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

        public AuthorsController(
            IAuthorCommandRepository commandRepo,
            IAuthorQueryRepository queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _queryRepo.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _queryRepo.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _commandRepo.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = newId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Author author)
        {
            if (id != author.Id) return BadRequest("ID mismatch");

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
