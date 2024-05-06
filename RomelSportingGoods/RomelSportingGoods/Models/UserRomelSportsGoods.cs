using System.ComponentModel.DataAnnotations;

namespace RomelSportingGoods.Models
{
    public class UserRomelSportsGoods
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;

    }
}
