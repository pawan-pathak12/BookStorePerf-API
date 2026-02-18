using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Queries
{
    public interface IBookQueryRepository
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookDetailsById(int bookId);
        Task<IEnumerable<Book>> GetBooksWithAuthor(string? authorName);
        Task<Book> GetBooksPaged();
    }
}
