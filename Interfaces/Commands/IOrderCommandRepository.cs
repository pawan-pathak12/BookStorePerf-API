using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Commands
{
    public interface IOrderCommandRepository
    {
        Task<int> PlaceOrder(Order order);
        Task UpdateOrder(int id, Order order);
        Task DeleteOrder(int id);
    }
}
