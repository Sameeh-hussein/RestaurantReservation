namespace RestaurantReservation.Db.Models
{
    public class Customer
    {
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public List<Reservation> reservations { get; set; }
    }

}
