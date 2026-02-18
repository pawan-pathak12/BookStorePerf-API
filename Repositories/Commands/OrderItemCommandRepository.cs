using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using Microsoft.EntityFrameworkCore;

namespace BookStorePerfApi.Repositories.Commands
{
    public class OrderItemCommandRepository : IOrderItemCommandRepository
    {
        private readonly AppDbContext _context;

        public OrderItemCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrderItemAsync(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task UpdateOrderItemAsync(int id, OrderItem item)
        {
            var entity = await _context.OrderItems.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"OrderItem {id} not found");

            _context.Entry(entity).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return;

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByOrderIdAsync(int orderId)
        {
            var items = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();

            if (items.Any())
            {
                _context.OrderItems.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
        }
    }

}