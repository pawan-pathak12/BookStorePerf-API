using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Queries
{
    public interface IOrderItemQueryRepository
    {
        Task<OrderItem?> GetByIdAsync(int id);
        Task<List<OrderItem>> GetByOrderIdAsync(int orderId);
    }
}
