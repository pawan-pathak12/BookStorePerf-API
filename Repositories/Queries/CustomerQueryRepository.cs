using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Queries;
using Dapper;

namespace BookStorePerfApi.Repositories.Queries
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly DapperContext _context;

        public CustomerQueryRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<Customer?> GetByIdAsync(int id)
        {
            using var conn = _context.OpenConnection();
            return await conn.QuerySingleOrDefaultAsync<Customer>(
                "SELECT * FROM Customers WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            using var conn = _context.OpenConnection();
            var customers = await conn.QueryAsync<Customer>("SELECT * FROM Customers");
            return customers.ToList();
        }

    }
}
