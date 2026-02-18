using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;

namespace BookStorePerfApi.Repositories.Commands
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly AppDbContext _context;

        public OrderCommandRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<int> PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task UpdateOrder(int id, Order order)
        {
            var entity = await _context.Orders.FindAsync(id);

            _context.Entry(entity).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
