namespace RestaurantReservation.Db.Models
{
    public class Reservation
    {
        public int reservationId { get; set; }
        public DateTime reservationDate { get; set; }
        public int partySize { get; set; }

        public int restaurantId { get; set; }
        public Restaurant reservation { get; set; }

        public int tableId { get; set; }
        public Table table { get; set; }

        public int customerID { get; set; }
        public Customer customer { get; set; }

        public List<Order> orders { get; set; }
    }

}
