using CsvHelper;
using PlayersAPI.Models;
using System.Globalization;

namespace PlayersAPI.Services
{
    public static class SeedData
    {
        static public void Initialize(AppDbContext context, string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Player>().ToList();
            Console.WriteLine(records);
            context.Players.AddRange(records);
            context.SaveChanges();
        }
    }
}
