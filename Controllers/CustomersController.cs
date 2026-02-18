using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStorePerfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerCommandRepository _commandRepo;
        private readonly ICustomerQueryRepository _queryRepo;

        public CustomersController(
            ICustomerCommandRepository commandRepo,
            ICustomerQueryRepository queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _queryRepo.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _queryRepo.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _commandRepo.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = newId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id) return BadRequest("ID mismatch");

            try
            {
                await _commandRepo.UpdateCustomerAsync(id, customer);
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
            await _commandRepo.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
