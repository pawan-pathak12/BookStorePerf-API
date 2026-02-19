using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Queries;
using Dapper;

namespace BookStorePerfApi.Repositories.Queries
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly DapperContext _context;

        public OrderQueryRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            using var connection = _context.OpenConnection();

            return await connection.QuerySingleOrDefaultAsync<Order>(
                @"SELECT Id, CustomerId, OrderDate, TotalAmount, Status, ShippingAddress /* etc */
              FROM Orders
              WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<List<Order>> GetAllAsync()
        {
            using var connection = _context.OpenConnection();

            var orders = await connection.QueryAsync<Order>(
                @"SELECT Id, CustomerId, OrderDate
              FROM Orders
              ORDER BY OrderDate DESC");

            return orders.ToList();
        }

        public async Task<List<Order>> GetByCustomerIdAsync(int customerId)
        {
            using var connection = _context.OpenConnection();

            var orders = await connection.QueryAsync<Order>(
                @"SELECT Id, CustomerId, OrderDate
              FROM Orders
              WHERE CustomerId = @CustomerId
              ORDER BY OrderDate DESC",
                new { CustomerId = customerId });

            return orders.ToList();
        }
    }
}
