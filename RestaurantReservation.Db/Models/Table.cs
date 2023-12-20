namespace RestaurantReservation.Db.Models
{
    public class Table
    {
        public int tableId { get; set; }
        public int capacity { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<Reservation> Restaurants { get; set; }
    }

}
