using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Queries;
using Dapper;

namespace BookStorePerfApi.Repositories.Queries
{
    public class OrderItemQueryRepository : IOrderItemQueryRepository
    {
        private readonly DapperContext _context;

        public OrderItemQueryRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            using var conn = _context.OpenConnection();
            return await conn.QuerySingleOrDefaultAsync<OrderItem>(
                "SELECT * FROM OrderItems WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<List<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            using var conn = _context.OpenConnection();
            var items = await conn.QueryAsync<OrderItem>(
                @"SELECT * FROM OrderItems 
              WHERE OrderId = @OrderId 
              ORDER BY Id",
                new { OrderId = orderId });
            return items.ToList();
        }
    }
}
