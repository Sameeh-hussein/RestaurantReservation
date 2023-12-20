namespace RestaurantReservation.Db.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }   
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string openingHours { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<Table> tables { get; set; }
        public List<Employee> employees { get; set; }
        public List<MenuItems> menuItems { get; set; }
    }
}
