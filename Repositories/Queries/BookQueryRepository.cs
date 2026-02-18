using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Queries;
using Dapper;

namespace BookStorePerfApi.Repositories.Queries
{
    public class BookQueryRepository : IBookQueryRepository
    {
        private readonly DapperContext _context;

        public BookQueryRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            using var connection = _context.OpenConnection();
            var query = "SELECT * FROM Books";
            var books = await connection.QueryAsync<Book>(query);
            return books;
        }

        public async Task<Book> GetBookDetailsById(int bookId)
        {
            using var connection = _context.OpenConnection();
            var query = "SELECT * FROM Books WHERE Id = @Id";
            var book = await connection.QuerySingleOrDefaultAsync<Book>(query, new { Id = bookId });
            return book;
        }

        public async Task<Book> GetBooksPaged()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthor(string? authorName)
        {
            using var connection = _context.OpenConnection();
            var query = "SELECT * FROM Books WHERE Author = @Author";
            var books = await connection.QueryAsync<Book>(query, new { Author = authorName });
            return books;
        }


    }
}
