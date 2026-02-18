using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;

namespace BookStorePerfApi.Repositories.Commands
{
    public class BookCommandRepository : IBookCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public BookCommandRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<int> AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateAsync(int id, Book book)
        {
            var existingBook = await _dbContext.Books.FindAsync(id);
            if (existingBook != null)
            {
                existingBook.Price = book.Price;
                existingBook.Title = book.Title;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
