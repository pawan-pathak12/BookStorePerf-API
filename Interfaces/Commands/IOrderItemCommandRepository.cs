using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Commands
{
    public interface IOrderItemCommandRepository
    {
        Task<int> AddOrderItemAsync(OrderItem item);
        Task UpdateOrderItemAsync(int id, OrderItem item);
        Task DeleteOrderItemAsync(int id);
        Task DeleteByOrderIdAsync(int orderId);
    }
}
