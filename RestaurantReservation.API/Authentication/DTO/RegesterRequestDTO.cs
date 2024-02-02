using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.Authentication.DTO
{
    public class RegesterRequestDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
