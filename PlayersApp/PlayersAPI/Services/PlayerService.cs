using PlayersAPI.Models;
using CsvHelper;
using System.Globalization;

namespace PlayersAPI.Services
{
    public interface IPlayerService
    {
        IEnumerable<Player> GetPlayers();

        Player? GetPlayerById(string playerId);

        IEnumerable<Player> GetPlayersByPage(int page, int pageCount);

        int PlayersCount();

    }
    public class PlayerService: IPlayerService
    {
        private readonly AppDbContext _context;

        public PlayerService(AppDbContext context)
        {
            _context = context;
        }

        public int PlayersCount() {
            return  _context.Players.Count();
        }
           
        public IEnumerable<Player> GetPlayers() => _context.Players.Take(10).ToList();

        public Player? GetPlayerById(string Id) => _context.Players.FirstOrDefault((p) => p.playerID == Id);

        
        public IEnumerable<Player> GetPlayersByPage(int page, int pageCount)
        {
         
           return _context.Players.Skip((page - 1) * pageCount).Take(pageCount).ToList();
        }
    }
}
