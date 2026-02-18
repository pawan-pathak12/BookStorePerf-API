using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Queries
{
    public interface IOrderQueryRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByCustomerIdAsync(int customerId);

    }
}
