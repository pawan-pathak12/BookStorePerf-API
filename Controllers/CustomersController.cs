using AutoMapper;
using BookStorePerfApi.DTOs;
using BookStorePerfApi.DTOs.Customers;
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
        private readonly IMapper _mapper;

        public CustomersController(
            ICustomerCommandRepository commandRepo,
            ICustomerQueryRepository queryRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            this._mapper = mapper;
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
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customer = _mapper.Map<Customer>(customerDto);
            var newId = await _commandRepo.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = newId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto customerDto)
        {
            if (id != customerDto.Id) return BadRequest("ID mismatch");
            var customer = _mapper.Map<Customer>(customerDto);

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
