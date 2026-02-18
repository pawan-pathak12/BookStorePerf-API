using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using BookStorePerfApi.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BookStorePerfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemCommandRepository _commandRepo;
        private readonly IOrderItemQueryRepository _queryRepo;

        public OrderItemsController(
            IOrderItemCommandRepository commandRepo,
            IOrderItemQueryRepository queryRepo)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            var items = await _queryRepo.GetByOrderIdAsync(orderId);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _queryRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _commandRepo.AddOrderItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = newId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderItem item)
        {
            if (id != item.Id) return BadRequest("ID mismatch");

            try
            {
                await _commandRepo.UpdateOrderItemAsync(id, item);
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
            await _commandRepo.DeleteOrderItemAsync(id);
            return NoContent();
        }

        [HttpDelete("order/{orderId}")]
        public async Task<IActionResult> DeleteByOrder(int orderId)
        {
            await _commandRepo.DeleteByOrderIdAsync(orderId);
            return NoContent();
        }
    }
}
