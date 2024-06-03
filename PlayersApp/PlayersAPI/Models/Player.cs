namespace PlayersAPI.Models
{
    public class Player
    {
        public required string playerID { get; set; }
        public int? birthYear { get; set; }
        public int? birthMonth { get; set; }
        public int? birthDay { get; set; }

        public required string birthCountry { get; set; }
        public string? birthState { get; set; }
        public string? birthCity { get; set; }

        public int? deathYear { get; set; }
        public int? deathMonth { get; set; }
        public int? deathDay { get; set; }

        public string? deathCountry { get; set; }
        public string? deathState { get; set; }
        public string? deathCity { get; set; }

        public required string nameFirst { get; set; }
        public required string nameLast { get; set; }
        public required string nameGiven { get; set; }

        public int? weight { get; set; }
        public  int? height { get; set; }
        public char? bats { get; set; }
        public char? throws { get; set; }

        public DateOnly? debut{ get; set; }
        public  DateOnly? finalGame { get; set; }
        public string? retroID { get; set; }
        public string? bbrefID { get; set; }

    }
}
