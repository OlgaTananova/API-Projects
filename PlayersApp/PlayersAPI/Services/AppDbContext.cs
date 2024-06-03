using Microsoft.EntityFrameworkCore;
using PlayersAPI.Models;

namespace PlayersAPI.Services
{
    public class AppDbContext : DbContext
    {

        public DbSet<Player> Players { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
