using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Queries
{
    public interface ICustomerQueryRepository
    {
        Task<Customer?> GetByIdAsync(int id);
        Task<List<Customer>> GetAllAsync();
    }
}
