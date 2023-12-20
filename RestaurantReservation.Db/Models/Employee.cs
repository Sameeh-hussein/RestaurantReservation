namespace RestaurantReservation.Db.Models
{
    public class Employee
    {
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string position { get; set; }

        public int restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<Order> orders { get; set; }
    }

}
