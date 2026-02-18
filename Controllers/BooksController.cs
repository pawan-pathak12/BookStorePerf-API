using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStorePerfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookCommandRepository _commandRepo;
        private readonly IBookQueryRepository _queryRepo;

        public BooksController(
            IBookCommandRepository commandRepo,
            IBookQueryRepository queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _queryRepo.GetAllBookAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _queryRepo.GetBookDetailsById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(string name)
        {
            var books = await _queryRepo.GetBooksWithAuthor(name);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _commandRepo.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = newId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest("ID mismatch");

            try
            {
                await _commandRepo.UpdateAsync(id, book);
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
            await _commandRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
