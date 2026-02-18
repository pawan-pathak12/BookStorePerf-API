using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStorePerfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderCommandRepository _commandRepo;
        private readonly IOrderQueryRepository _queryRepo;

        public OrdersController(
            IOrderCommandRepository commandRepo,
            IOrderQueryRepository queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _queryRepo.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _queryRepo.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var orders = await _queryRepo.GetByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _commandRepo.PlaceOrder(order);
            return CreatedAtAction(nameof(GetById), new { id = newId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest("ID mismatch");

            try
            {
                await _commandRepo.UpdateOrder(id, order);
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
            await _commandRepo.DeleteOrder(id);
            return NoContent();
        }
    }
}
