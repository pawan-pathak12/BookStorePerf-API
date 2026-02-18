using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Queries;
using Dapper;

namespace BookStorePerfApi.Repositories.Queries
{
    public class AuthorQueryRepository : IAuthorQueryRepository
    {
        private readonly DapperContext _context;

        public AuthorQueryRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<Author?> GetByIdAsync(int id)
        {
            using var connection = _context.OpenConnection();
            return await connection.QuerySingleOrDefaultAsync<Author>(
                "SELECT * FROM Authors WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<List<Author>> GetAllAsync()
        {
            using var connection = _context.OpenConnection();
            var result = await connection.QueryAsync<Author>("SELECT * FROM Authors");
            return result.ToList();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = _context.OpenConnection();
            return await connection.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS (SELECT 1 FROM Authors WHERE Id = @Id) THEN 1 ELSE 0 END",
                new { Id = id });
        }
    }
}
