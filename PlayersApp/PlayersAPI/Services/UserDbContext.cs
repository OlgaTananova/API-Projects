using Microsoft.EntityFrameworkCore;
using PlayersAPI.Models;

namespace PlayersAPI.Services
{
    public class UserDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }
}