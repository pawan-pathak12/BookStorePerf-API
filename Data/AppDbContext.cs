using Microsoft.EntityFrameworkCore;

namespace BookStorePerfApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


    }
}
