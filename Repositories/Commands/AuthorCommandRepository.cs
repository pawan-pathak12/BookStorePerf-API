using BookStorePerfApi.Data;
using BookStorePerfApi.Entities;
using BookStorePerfApi.Interfaces.Commands;
using Microsoft.EntityFrameworkCore;

namespace BookStorePerfApi.Repositories.Commands
{
    public class AuthorCommandRepository : IAuthorCommandRepository
    {
        private readonly AppDbContext _context;

        public AuthorCommandRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<int> AddAuthorAsync(Author author)

        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }

        public async Task UpdateAuthorAsync(int id, Author author)
        {
            var entity = await _context.Authors.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Author {id} not found");

            _context.Entry(entity).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
    }
}
