using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PlayersAPI.Services;
using PlayersAPI;
using PlayersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PlayersAPITests
{
    public class PlayersControllerTests { 
    
        private readonly Mock<IPlayerService> _mockPlayerService;
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _mockPlayerService = new Mock<IPlayerService>();
            _controller = new PlayersController(_mockPlayerService.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfPlayers()
        {
            // Arrange
            var players = new List<Player> { new Player { playerID = "1", birthCountry="USA", nameFirst="John", nameGiven="", nameLast="Smith" }, new Player { playerID = "2", birthCountry = "USA", nameFirst = "John", nameGiven = "", nameLast = "Smith" } };
            _mockPlayerService.Setup(service => service.GetPlayers()).Returns(players);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Player>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void GetPlayer_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.GetPlayerById("1")).Returns((Player)null);

            // Act
            var result = _controller.GetPlayer("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetPlayer_ReturnsOkResult_WithPlayer()
        {
            // Arrange
            var player = new Player  { playerID = "1", birthCountry = "USA", nameFirst = "John", nameGiven = "", nameLast = "Smith"  };
            _mockPlayerService.Setup(service => service.GetPlayerById("1")).Returns(player);

            // Act
            var result = _controller.GetPlayer("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Player>(okResult.Value);
            Assert.Equal("1", returnValue.playerID);
        }

        [Fact]
        public void GetPlayersByPage_ReturnsBadRequest_WhenPageIsLessThan1()
        {
            // Act
            var result = _controller.GetPlayersByPage(0);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Page number must be at least 1.", badRequestResult.Value);
        }

        [Fact]
        public void GetPlayersByPage_ReturnsBadRequest_WhenPageExceedsTotalPages()
        {
            // Arrange
            _mockPlayerService.Setup(service => service.PlayersCount()).Returns(50);

            // Act
            var result = _controller.GetPlayersByPage(6, 10);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("The page you requested exceeds the total number of pages.", badRequestResult.Value);
        }

        [Fact]
        public void GetPlayersByPage_ReturnsOkResult_WithPagedPlayers()
        {
            // Arrange
            var players = new List<Player> { new Player { playerID = "1", birthCountry = "USA", nameFirst = "John", nameGiven = "", nameLast = "Smith" }, new Player { playerID = "2", birthCountry = "USA", nameFirst = "John", nameGiven = "", nameLast = "Smith" } };
            _mockPlayerService.Setup(service => service.GetPlayersByPage(1, 10)).Returns(players);

            // Act
            var result = _controller.GetPlayersByPage(1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}