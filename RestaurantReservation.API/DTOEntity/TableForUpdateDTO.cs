using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class TableForUpdateDTO
    {
        public int capacity { get; set; }

        public int restaurantId { get; set; }

        public List<Reservation> reservations { get; set; }
    }
}
