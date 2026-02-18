using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;

namespace BookStorePerfApi.Repositories.Commands
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly AppDbContext _context;

        public CustomerCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task UpdateCustomerAsync(int id, Customer customer)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Customer {id} not found");

            _context.Entry(entity).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
