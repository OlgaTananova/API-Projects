using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayersAPI.Models;
using PlayersAPI.Services;
using SQLitePCL;

namespace PlayersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {

        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService service)
        {
            playerService = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> Get()
        {
            return Ok(playerService.GetPlayers());
        }

        [HttpGet("{playerId}")]
        public ActionResult<Player> GetPlayer(string playerId)
        {
            var player = playerService.GetPlayerById(playerId);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpGet("/page/{page}")]
        public ActionResult<object> GetPlayersByPage(int page, int pageCount = 100)
        {
            if (page < 1)
            {
                return BadRequest("Page number must be at least 1.");
            }
            int totalCount = playerService.PlayersCount();
            int totalPages = (int) Math.Ceiling((totalCount / (double) pageCount));

            if (page > totalPages && totalPages > 0)
            {
                return BadRequest("The page you requested exceeds the total number of pages.");
            }

            IEnumerable<Player> players = playerService.GetPlayersByPage(page, pageCount);

            if (!players.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalCountOfPlayers = totalCount,
                TotalCountOfPages = totalPages,
                CurrentPage = page,
                PlayersPerPage = pageCount,
                Players = players
            });
           
        }
    }
}
