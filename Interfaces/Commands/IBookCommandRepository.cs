using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Commands
{
    public interface IBookCommandRepository
    {
        Task<int> AddAsync(Book book);
        Task UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);
    }
}
