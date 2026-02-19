using AutoMapper;
using BookStorePerfApi.DTOs.Orders;
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
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderCommandRepository commandRepo,
            IOrderQueryRepository queryRepo, IMapper mapper)
        {
            _commandRepo = commandRepo;
            _queryRepo = queryRepo;
            this._mapper = mapper;
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
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);
            var newId = await _commandRepo.PlaceOrder(order);
            return CreatedAtAction(nameof(GetById), new { id = newId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto orderDto)
        {
            if (id != orderDto.Id) return BadRequest("ID mismatch");
            var order = _mapper.Map<Order>(orderDto);

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
