using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayersAPI.Models;
using PlayersAPI.Services;
using System.Security.Claims;

namespace PlayersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var existingUser = await _userService.GetUser(email);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            User? user = await _userService.Register(email, password);
            if (user != null) return Ok("User registered successfully.");
            return BadRequest("Failed to register a new user.");

            
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            var userEmail = User.FindFirst("Email")?.Value;
            var userId = User.FindFirst("UserId")?.Value;
            return Ok(new { Email = userEmail, UserId = userId});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            User? user = await _userService.GetUser(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }


            var claims = new List<Claim>
        {
            new Claim("Email", user.Email),
            new Claim("UserId", user.Id.ToString()),
        };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            await HttpContext.SignInAsync("CookieAuthentication", claimsPrincipal, authProperties);

            return Ok("Login successful.");


        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return Ok("Logged out successfully.");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Forbid("You do not have access to this resource.");
        }
    }
}
