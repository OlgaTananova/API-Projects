using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlayersAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [MinLength(3)]
        [MaxLength(10)]
        public required string PasswordHash { get; set; }
    }
}
