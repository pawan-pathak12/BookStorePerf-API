using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Commands
{
    public interface IAuthorCommandRepository
    {
        Task<int> AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(int id, Author author);
        Task DeleteAuthorAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
