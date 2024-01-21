using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.API.DTOEntity
{
    public class CustomerForCreationDTO
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "You must input a name !!")]
        public string email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
    }
}
