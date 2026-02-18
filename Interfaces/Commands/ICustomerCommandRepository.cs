using BookStorePerfApi.Entities;

namespace BookStorePerfApi.Interfaces.Commands
{
    public interface ICustomerCommandRepository
    {
        Task<int> AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(int id, Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
