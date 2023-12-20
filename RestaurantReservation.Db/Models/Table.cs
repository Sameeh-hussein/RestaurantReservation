namespace RestaurantReservation.Db.Models
{
    public class Table
    {
        public int tableId { get; set; }
        public int capacity { get; set; }

        public int restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<Reservation> reservations { get; set; }
    }

}
