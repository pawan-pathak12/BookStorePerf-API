using AutoMapper;
using BookStorePerfApi.DTOs.OrderItems;
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
        private readonly IMapper _mapper;

        public OrderItemsController(
            IOrderItemCommandRepository commandRepo,
            IOrderItemQueryRepository queryRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            this._mapper = mapper;
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
        public async Task<IActionResult> Create([FromBody] CreateOrderItemDto itemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = _mapper.Map<OrderItem>(itemDto);
            var newId = await _commandRepo.AddOrderItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = newId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemDto itemDto)
        {
            if (id != itemDto.Id) return BadRequest("ID mismatch");
            var item = _mapper.Map<OrderItem>(itemDto);
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
