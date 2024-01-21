using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class CustomerDTO
    {
        public int customerId { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;

    }
}
