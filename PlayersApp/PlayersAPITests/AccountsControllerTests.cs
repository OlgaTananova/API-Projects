using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlayersAPI.Controllers;
using PlayersAPI.Models;
using PlayersAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlayersAPITests
{
    public class AccountsControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly AccountController _accountController;
        public AccountsControllerTests()
        {
            _userService = new Mock<IUserService>();
            _accountController = new AccountController(_userService.Object);

            // Setting up the HttpContext to mock the HttpContext for the controller
            var context = new DefaultHttpContext();
            _accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = context
            };
        }

        [Fact]

        public async Task RegisterNewUser_ReturnsOkResult_WithANewUser()
        {
            // Arrange
            string email = "olga@yandex.ru";
            string password = "123";
            _userService.Setup(s => s.GetUser(It.IsAny<string>())).ReturnsAsync((User)null);
            _userService.Setup(s => s.Register(email, password)).ReturnsAsync(new User() { Email = email, Id = 1, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)});


            // Act 

            var result = await _accountController.Register(email, password);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully.", okResult.Value);

        }

        [Fact]
        public async Task Register_UserAlreadyExists_ReturnsConflict()
        {
            // Arrange 

            string email = "olga@yandex.ru";
            string password = "123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _userService.Setup(s => s.GetUser(email)).ReturnsAsync(new User { Email = email, Id = 1, PasswordHash = password });


            //Act 

            var result = await _accountController.Register(email, password);

            //Assert

            var conflictResult = Assert.IsType<ConflictObjectResult>(result);

            Assert.Equal("Username already exists.", conflictResult.Value);

        }

        [Fact]
        public async Task Register_InvalidData_ReturnsBadRequest()
        {
            // Arrange

            string email = "jgs";
            string password = "123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            _userService.Setup(s => s.GetUser(email)).ReturnsAsync((User)null);

            // Act

            var result = await _accountController.Register(email, password);

            //Assert

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to register a new user.", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            string email = "jgs";
            string password = "123";
            _userService.Setup(s => s.GetUser(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _accountController.Login(email, password);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);
        }

        [Fact]
        public async Task Login_ValidUser_ReturnsOk()
        {
            // Arrange
            string email = "jgs";
            string password = "123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User { Email = email, PasswordHash = passwordHash, Id = 1 };
            _userService.Setup(s => s.GetUser(It.IsAny<string>())).ReturnsAsync(user);

            // Mocking SignInAsync method
            var mockAuth = new Mock<IAuthenticationService>();
            _accountController.ControllerContext.HttpContext.RequestServices = new ServiceCollection()
                .AddSingleton(mockAuth.Object)
                .BuildServiceProvider();
            
            mockAuth.Setup(x => x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.Login(email, password);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Login successful.", okResult.Value);
        }

        [Fact]

        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            string email = "jgs";
            string password = "123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword("hggaf"); // Invalid hash

            var user = new User { Email = email, PasswordHash = passwordHash, Id = 1 };
            _userService.Setup(s => s.GetUser(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _accountController.Login(email, password);

            //Assert

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);

        }
    }
}


