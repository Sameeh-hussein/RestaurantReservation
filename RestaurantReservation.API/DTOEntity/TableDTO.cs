using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.DTOEntity
{
    public class TableDTO
    {
        public int tableId { get; set; }
        public int capacity { get; set; }

        public int restaurantId { get; set; }

        public List<Reservation> reservations { get; set; }
    }
}
