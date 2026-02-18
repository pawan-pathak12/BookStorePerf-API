using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Queries
{
    public interface IAuthorQueryRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
    }
}
