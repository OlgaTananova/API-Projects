using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayersAPI.Models;

namespace PlayersAPI.Services
{
    public interface IUserService
    {
        public Task<User?> Register(string email, string password);
        public Task<User?> GetUser(string email);

    }


    public class UserService: IUserService
    {

        private UserDbContext _userContext;

        public UserService(UserDbContext userContext)
        {
            _userContext = userContext;

        }

        public async Task<User?> Register(string email, string password)
        {
           
            // hashed password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            User user = new User { Email = email, PasswordHash = hashedPassword };
            _userContext.Users.Add(user);

           int result = await _userContext.SaveChangesAsync();


            if (result > 0) return user;
            return null;
        }

        public async Task<User?> GetUser(string email)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

    }
}
